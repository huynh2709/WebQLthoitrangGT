using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyClass.DAO;
using MyClass.Models;
using WebThoiTrangGioiTre.Library;

namespace WebThoiTrangGioiTre.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
		ProductDAO productDAO = new ProductDAO();
		ProductCategoryDAO productCategoryDAO = new ProductCategoryDAO();
		LinkDAO linkDAO = new LinkDAO();

		// GET: Admin/Product
		public ActionResult Index()
        {
            return View(productDAO.getList("Index"));
        }

        // GET: Admin/Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = productDAO.getRow(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Admin/Product/Create
        public ActionResult Create()
        {
			ViewBag.ListCate = new SelectList(productCategoryDAO.getList("Index"), "Id", "Name", 0);
			return View();
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
		[ValidateInput(false)]
		public ActionResult Create( Product product)
        {
            if (ModelState.IsValid)
            {
				product.Slug = XString.Str_Slug(product.Name);
				if(product.IdCategory == null)
				{
					product.IdCategory = 0;
				}
				
				//upload file
				var img = Request.Files["img"];//lấy thông tin file
				if(img.ContentLength != 0)
				{
					string[] FileExtentions = new string[] { ".jpg", ".jepg", ".png", ".gif", ".webp", ".htm" };
					//kiểm tra file
					if (FileExtentions.Contains(img.FileName.Substring(img.FileName.LastIndexOf("."))))
					{
						//upload hình
						string imgName = product.Slug + img.FileName.Substring(img.FileName.LastIndexOf("."));
						product.Image = imgName;
						string PathDir = "~/Public/images/products/";
						string PathFile = Path.Combine(Server.MapPath(PathDir), imgName);
						img.SaveAs(PathFile);
					}
				}

				product.CreatedAt = DateTime.Now;
				if(productDAO.Inset(product) ==1)
				{
					Link link = new Link();
					link.NameLink = product.Slug;
					link.TableId = product.Id;
					link.Type = "product";
					linkDAO.Inset(link);
				}
				TempData["message"] = new XMessage("success", "Thêm thành công!");
				return RedirectToAction("Index","Product");
            }
			ViewBag.ListCate = new SelectList(productCategoryDAO.getList("Index"), "Id", "Name", 0);
			return View(product);
        }

        // GET: Admin/Product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = productDAO.getRow(id);
            if (product == null)
            {
                return HttpNotFound();
            }
			ViewBag.ListCate = new SelectList(productCategoryDAO.getList("Index"), "Id", "Name", 0);
			return View(product);
        }

       [HttpPost]
        [ValidateAntiForgeryToken]
		[ValidateInput(false)]
		public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
				product.Slug = XString.Str_Slug(product.Name);
				if (product.IdCategory == null)
				{
					product.IdCategory = 0;
				}
				//upload file
				var img = Request.Files["img"];//lấy thông tin file
				if (img.ContentLength != 0)
				{
					string[] FileExtentions = new string[] { ".jpg", ".jepg", ".png", ".gif", ".webp", ".htm" };
					//kiểm tra file
					if (FileExtentions.Contains(img.FileName.Substring(img.FileName.LastIndexOf("."))))
					{
						//upload hình
						string imgName = product.Slug + img.FileName.Substring(img.FileName.LastIndexOf("."));
						product.Image = imgName;
						string PathDir = "~/Public/images/products/";
						string PathFile = Path.Combine(Server.MapPath(PathDir), imgName);

						//Xóa file
						if(product.Image.Length > 0)
						{
							string DelPath = Path.Combine(Server.MapPath(PathDir), product.Image);
							System.IO.File.Delete(DelPath);
						}	

						img.SaveAs(PathFile);
					}
				}

				product.CreatedAt = DateTime.Now;
				product.UpdatedAt = DateTime.Now;
				if (productDAO.Update(product) == 1)
				{
					Link link = linkDAO.getRow(product.Id, "product");
					link.NameLink = product.Slug;
					linkDAO.Update(link);

				}
				TempData["message"] = new XMessage("success", "Cập nhật thành công!");
				return RedirectToAction("Index");
            }
			ViewBag.ListCate = new SelectList(productCategoryDAO.getList("Index"), "Id", "Name", 0);
			return View(product);
        }

        // GET: Admin/Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = productDAO.getRow(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = productDAO.getRow(id);
			Link link = linkDAO.getRow(product.Id, "product");
			//Xóa hình ảnh
			string PathDir = "~/Public/images/products/";
			//Xóa file
			if (product.Image != null)
			{
				string DelPath = Path.Combine(Server.MapPath(PathDir), product.Image);
				System.IO.File.Delete(DelPath);
			}

			if(productDAO.Delete(product)==1)
			{
				linkDAO.Delete(link);
			}
			TempData["message"] = new XMessage("success", "Xóa thành công!");
			return RedirectToAction("Index");
        }
    }
}
