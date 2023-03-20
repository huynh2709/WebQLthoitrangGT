using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyClass.Models;
using MyClass.DAO;
using WebThoiTrangGioiTre.Library;

namespace WebThoiTrangGioiTre.Areas.Admin.Controllers
{
    public class ProductCategoryController : Controller
    {
		ProductCategoryDAO productcategoryDAO = new ProductCategoryDAO();
		LinkDAO linkDAO = new LinkDAO();
		MenuDAO menuDAO = new MenuDAO();

		// GET: Admin/ProductCategory
		public ActionResult Index()
        {
			return View(productcategoryDAO.getList("Index"));
        }

        // GET: Admin/ProductCategory/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductCategory productCategory = productcategoryDAO.getRow(id);
            if (productCategory == null)
            {
                return HttpNotFound();
            }
            return View(productCategory);
        }

        // GET: Admin/ProductCategory/Create
        public ActionResult Create()
        {
			ViewBag.Listmenu = new SelectList(menuDAO.getList("Index"), "Id", "Name", 0);
			return View();
        }

		[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductCategory productCategory, ProductCategoryDAO productCategoryDAO)
        {
			
			if (ModelState.IsValid)
            {
				productCategory.Slug = XString.Str_Slug(productCategory.Name);
				//productCategory.CreateUserId = Convert.ToInt32(Session["UserId"].ToString());
				productCategory.CreatedAt = DateTime.Now;
				if (productcategoryDAO.Inset(productCategory) == 1)
				{
					Link link = new Link();
					link.NameLink = productCategory.Slug;
					link.TableId = productCategory.Id;
					link.Type = "ProductCategory";
					linkDAO.Inset(link);
				}
				TempData["message"] = new XMessage("success", "Thêm thành công!");
				return RedirectToAction("Index");
            }
			ViewBag.Listmenu = new SelectList(menuDAO.getList("Index"), "Id", "Name", 0);
			return View(productCategory);
        }

        // GET: Admin/ProductCategory/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductCategory productCategory = productcategoryDAO.getRow(id);
            if (productCategory == null)
            {
                return HttpNotFound();
            }
			ViewBag.Listmenu = new SelectList(menuDAO.getList("Index"), "Id", "Name", 0);
			return View(productCategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( ProductCategory productCategory)
        {
            if (ModelState.IsValid)
            {
				productCategory.Slug = XString.Str_Slug(productCategory.Name);
				//productCategory.UpdatedUserId = Convert.ToInt32(Session["UserId"].ToString());
				productCategory. UpdatedAt= DateTime.Now;
				if (productcategoryDAO.Update(productCategory) == 1)
				{
					Link link = linkDAO.getRow(productCategory.Id, "ProductCategory");
					link.NameLink = productCategory.Slug;
					linkDAO.Update(link);

				}
				TempData["message"] = new XMessage("success", "Cập nhật thành công!");
				return RedirectToAction("Index");
            }
			ViewBag.ListCate = new SelectList(menuDAO.getList("Index"), "Id", "Name", 0);
			return View(productCategory);
        }

        // GET: Admin/ProductCategory/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductCategory productCategory = productcategoryDAO.getRow(id);
            if (productCategory == null)
            {
                return HttpNotFound();
            }
            return View(productCategory);
        }

        // POST: Admin/ProductCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductCategory productCategory = productcategoryDAO.getRow(id);
			Link link = linkDAO.getRow(productCategory.Id, "ProductCategory");
			if (productcategoryDAO.Delete(productCategory) == 1)
			{
				linkDAO.Delete(link);
			}
			TempData["message"] = new XMessage("success", "Xóa thành công!");
			return RedirectToAction("Index");
        }
    }
}
