using MyClass.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyClass.Models;

namespace WebThoiTrangGioiTre.Controllers
{
    public class GioHangController : Controller
    {
		private DBContext _db = new DBContext();
		ProductDAO productDAO = new ProductDAO();
		UserDAO userDAO = new UserDAO();
		OrderDAO orderDAO = new OrderDAO();
		OrderdetailsDAO orderdetailsDAO = new OrderdetailsDAO();

		// GET: Cart
		public ActionResult Index()
        {
			List<CartItem> giohang = Session["giohang"] as List<CartItem>;

			int tongThanhToan = 0;
			if (giohang != null && giohang.Count > 0)
			{
			giohang.ForEach((CartItem item) => {
				tongThanhToan += item.DonGia;
			});

			ViewBag.tongThanhToan = tongThanhToan.ToString("#,##0").Replace(',', '.');
			}

			return View(giohang);
        }


		public RedirectToRouteResult ThemVaoGio(int SanPhamID)
		{
			if (Session["giohang"] == null) // Nếu giỏ hàng chưa được khởi tạo
			{
				Session["giohang"] = new List<CartItem>();  // Khởi tạo Session["giohang"] là 1 List<CartItem>
			}

			List<CartItem> giohang = Session["giohang"] as List<CartItem>;  // Gán qua biến giohang dễ code  

			// Kiểm tra xem sản phẩm khách đang chọn đã có trong giỏ hàng chưa

			if (giohang.FirstOrDefault(m => m.SanPhamID == SanPhamID) == null) // ko co sp nay trong gio hang
			{
				Product product = productDAO.getRow(SanPhamID);
				//P sp = db.SanPhams.Find(SanPhamID);  // tim sp theo sanPhamID

				CartItem newItem = new CartItem()
				{
					SanPhamID = SanPhamID,
					TenSanPham = product.Name,
					Gia = (int)product.Price,
					Giam = (int)product.Sale,
					SoLuong = 1,
					Hinh = product.Image,
					

				};  // Tạo ra 1 CartItem mới

				giohang.Add(newItem);  // Thêm CartItem vào giỏ 
			}
			else
			{
				// Nếu sản phẩm khách chọn đã có trong giỏ hàng thì không thêm vào giỏ nữa mà tăng số lượng lên.
				CartItem cardItem = giohang.FirstOrDefault(m => m.SanPhamID == SanPhamID);
				cardItem.SoLuong++;
			}

			// Action này sẽ chuyển hướng về trang chi tiết sp khi khách hàng đặt vào giỏ thành công. Bạn có thể chuyển về chính trang khách hàng vừa đứng bằng lệnh return Redirect(Request.UrlReferrer.ToString()); nếu muốn.
			return RedirectToAction("Index", "GioHang", new { id = SanPhamID });
		}


		public RedirectToRouteResult SuaSoLuong(int? SanPhamID, int soluongmoi)
		{
			// tìm carditem muon sua
			List<CartItem> giohang = Session["giohang"] as List<CartItem>;
			CartItem itemSua = giohang.FirstOrDefault(m => m.SanPhamID == SanPhamID);
			if (itemSua != null)
			{
				itemSua.SoLuong = soluongmoi;
			}
			return RedirectToAction("Index");

		}

		public RedirectToRouteResult XoaKhoiGio(int SanPhamID)
		{
			List<CartItem> giohang = Session["giohang"] as List<CartItem>;
			CartItem itemXoa = giohang.FirstOrDefault(m => m.SanPhamID == SanPhamID);
			if (itemXoa != null)
			{
				giohang.Remove(itemXoa);
			}
			return RedirectToAction("Index");
		}

		public ActionResult ThanhToan()
		{

			List<CartItem> giohang = Session["giohang"] as List<CartItem>;
			//Kiểm tra đăng nhập trang người dùng ==>customer

			int tongThanhToan = 0;
			if (giohang.Count > 0)
			{
				giohang.ForEach((CartItem item) => {
					tongThanhToan += item.DonGia;
				});

				ViewBag.tongThanhToan = tongThanhToan.ToString("#,##0").Replace(',', '.');
			}

			if (Session["idUser"] == null)
			{
				return Redirect("~/dang-nhap");// chuyển đến url
			}
			int userid = int.Parse(Session["idUser"].ToString());// mã người đăng nhập
			User user = userDAO.getRow(userid);
			ViewBag.user = user;

			return View("ThanhToan", giohang);
		}

		public ActionResult DatHang(FormCollection field)
		{
			int userid = int.Parse(Session["idUser"].ToString());// mã người đăng nhập
			User user = userDAO.getRow(userid);
			//String note = field["Note"];
			//tạo đói tượng đơn hàng
			Order order = new Order();
			order.IdCustomer = userid;
			order.NameCustomer = user.FirstName;
			order.Address = user.Address;
			order.Phone = user.Phone;
			order.Email = user.Email;
			order.Note = user.Note;
			order.CreatedAt = DateTime.Now;
			order.Status = 1;// ddown hang moiws them vao
			if (orderDAO.Inset(order) == 1)
			{
				// Theem vhi tiet don hang
				List<CartItem> giohang = Session["giohang"] as List<CartItem>;
				foreach (CartItem cartItem in giohang)
				{
					Orderdetails orderdetails = new Orderdetails();
					orderdetails.OrderID = order.Id;
					orderdetails.ProductID = cartItem.SanPhamID;
					orderdetails.NameProduct = cartItem.TenSanPham;
					orderdetails.Price = cartItem.Giam;
					orderdetails.quantity = cartItem.SoLuong;
					orderdetails.Amount = cartItem.DonGia;
					orderdetails.CreatedAt = DateTime.Now;
					orderdetailsDAO.Inset(orderdetails);//Lưu
					
				}
				
			}
			//orderDAO.Delete(giohang);
			Session["giohang"] = null;
			return View("Thanhcong");
		}
		public ActionResult Thanhcong()
		{
			return View("Index");
		}

	}
}