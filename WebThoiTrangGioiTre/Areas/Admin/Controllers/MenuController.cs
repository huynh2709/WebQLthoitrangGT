using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using MyClass.DAO;
using MyClass.Models;
using Menu = MyClass.Models.Menu;

namespace WebThoiTrangGioiTre.Areas.Admin.Controllers
{
    public class MenuController : Controller
    {
		ProductCategoryDAO productCategoryDAO = new ProductCategoryDAO();
		NewsCategoryDAO newsCategoryDAO = new NewsCategoryDAO();
		NewsDAO newsDAO = new NewsDAO();
		MenuDAO menuDAO = new MenuDAO();
        // GET: Admin/Menu
        public ActionResult Index()
        {
			ViewBag.ListProductCategory = productCategoryDAO.getList("Index");
			ViewBag.ListNewsCategory = newsCategoryDAO.getList("Index");
			ViewBag.ListNews = newsDAO.getList("Index", "Page");

			return View(menuDAO.getList("Index"));
        }
		[HttpPost]
		public ActionResult Index(FormCollection form)
		{
			//Productcategory
			if(!string.IsNullOrEmpty(form["ThemCategory"]))
			{
				if(!string.IsNullOrEmpty(form["itemcate"]))
				{
					var listitem = form["itemcate"];
					var listarr = listitem.Split(',');
					foreach(var row in listarr)
					{
						//Id của category
						int id = int.Parse(row);//ep kieu
						ProductCategory productCategory = productCategoryDAO.getRow(id);
						Menu menu = new Menu();
						menu.Name = productCategory.Name;
						menu.TableId = productCategory.Id;
						menu.Link = productCategory.Slug;
						menu.ParentId = productCategory.ParentId;
						menu.TypeMenu = "ProductCategory";
						menu.Position = form["Position"];
						menu.CreatedAt = DateTime.Now;
						menuDAO.Insert(menu);
					}
					TempData["message"] = new XMessage("success", "Thêm menu thành công!");
				}
				else
				{
					TempData["message"] = new XMessage("danger", "Chưa chọn danh mục sản phẩm");
				}
			}

			//ThemNewsCategory
			if (!string.IsNullOrEmpty(form["ThemNewsCategory"]))
			{
				if (!string.IsNullOrEmpty(form["itemnewscate"]))
				{
					var listitem = form["itemnewscate"];
					var listarr = listitem.Split(',');
					foreach (var row in listarr)
					{
						//Id của category
						int id = int.Parse(row);//ep kieu
						NewsCategory newsCategory = newsCategoryDAO.getRow(id);
						Menu menu = new Menu();
						menu.Name = newsCategory.Name;
						menu.TableId = newsCategory.Id;
						menu.Link = newsCategory.Slug;
						menu.ParentId = newsCategory.ParentId;
						menu.TypeMenu = "NewsCategory";
						menu.Position = form["Position"];
						menu.CreatedAt = DateTime.Now;
						menuDAO.Insert(menu);
					}
					TempData["message"] = new XMessage("success", "Thêm menu thành công!");
				}
				else
				{
					TempData["message"] = new XMessage("danger", "Chưa chọn danh mục mẫu tin");
				}
			}

			//Theem page
			if (!string.IsNullOrEmpty(form["ThemPage"]))
			{
				if (!string.IsNullOrEmpty(form["itempage"]))
				{
					var listitem = form["itempage"];
					var listarr = listitem.Split(',');
					foreach (var row in listarr)
					{
						//Id của category
						int id = int.Parse(row);//ep kieu
						News newss = newsDAO.getRow(id);
						Menu menu = new Menu();
						menu.Name = newss.Name;
						menu.Link = newss.Slug;
						menu.ParentId = 0;
						menu.TableId = newss.Id;
						menu.TypeMenu = "Page";
						menu.Position = form["Position"];
						menu.CreatedAt = DateTime.Now;
						menuDAO.Insert(menu);
					}
					TempData["message"] = new XMessage("success", "Thêm menu thành công!");
				}
				else
				{
					TempData["message"] = new XMessage("danger", "Chưa chọn trang mẫu tin");
				}
			}

			//Thêm ThemCustom
			if (!string.IsNullOrEmpty(form["ThemCustom"]))
			{
				if(!string.IsNullOrEmpty(form["name"]) && !string.IsNullOrEmpty(form["link"]))
				{
					Menu menu = new Menu();
					menu.Name = form["name"];
					menu.Link = form["link"];
					menu.ParentId = 0;
					menu.TypeMenu = "Custom";
					menu.Position = form["Position"];
					menu.CreatedAt = DateTime.Now;
					menuDAO.Insert(menu);
					TempData["message"] = new XMessage("success", "Thêm menu thành công!");
				}
				else
				{
					TempData["message"] = new XMessage("danger", "Chưa nhập đủ thông tin");
				}
			}

			return RedirectToAction("Index", "Menu");
		}

		public ActionResult Edit(int? id)
		{
			ViewBag.ListMenu = new SelectList(menuDAO.getList("Index"), "Id", "Name");
			Menu menu = menuDAO.getRow(id);
			return View("Edit", menu);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(Menu menu)
		{
			menu.CreatedAt = DateTime.Now;
			menu.UpdatedAt = DateTime.Now;
			menuDAO.Update(menu);
			ViewBag.ListMenu = new SelectList(menuDAO.getList("Index"), "Id", "Name");
			TempData["message"] = new XMessage("success", "Cập nhật thành công!");
			return RedirectToAction("Index",menu);
		}
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Menu menu = menuDAO.getRow(id);
			if (menu == null)
			{
				return HttpNotFound();
			}
			return View(menu);
		}

		// GET: Admin/News/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Menu menu = menuDAO.getRow(id);
			if (menu == null)
			{
				return HttpNotFound();
			}
			return View(menu);
		}

		// POST: Admin/News/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			Menu menu = menuDAO.getRow(id);
			menuDAO.Delete(menu);
			return RedirectToAction("Index");
		}
	}
}