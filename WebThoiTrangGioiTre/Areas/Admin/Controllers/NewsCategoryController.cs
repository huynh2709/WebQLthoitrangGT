using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyClass.DAO;
using MyClass.Models;
using WebThoiTrangGioiTre.Library;

namespace WebThoiTrangGioiTre.Areas.Admin.Controllers
{
    public class NewsCategoryController : Controller
    {
		NewsCategoryDAO newsCategoryDAO = new NewsCategoryDAO();
		LinkDAO linkDAO = new LinkDAO();
		MenuDAO menuDAO = new MenuDAO();

		// GET: Admin/NewsCategory
		public ActionResult Index()
        {
            return View(newsCategoryDAO.getList("Index"));
        }

        // GET: Admin/NewsCategory/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsCategory newsCategory = newsCategoryDAO.getRow(id);
			if (newsCategory == null)
            {
                return HttpNotFound();
            }
            return View(newsCategory);
        }

        // GET: Admin/NewsCategory/Create
        public ActionResult Create()
        {
			ViewBag.Listmenu = new SelectList(menuDAO.getList("Index"), "Id", "Name", 0);
			return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NewsCategory newsCategory)
        {
            if (ModelState.IsValid)
            {
				newsCategory.Slug = XString.Str_Slug(newsCategory.Name);
				//newsCategory.CreateUserId = Convert.ToInt32(Session["UserId"].ToString());
				newsCategory.CreatedAt = DateTime.Now;
				if (newsCategoryDAO.Inset(newsCategory) == 1)
				{
					Link link = new Link();
					link.NameLink = newsCategory.Slug;
					link.TableId = newsCategory.Id;
					link.Type = "NewsCategory";
					linkDAO.Inset(link);
				}
				TempData["message"] = new XMessage("success", "Thêm thành công!");
				return RedirectToAction("Index");
            }
			ViewBag.Listmenu = new SelectList(menuDAO.getList("Index"), "Id", "Name", 0);
			return View(newsCategory);
        }

        // GET: Admin/NewsCategory/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsCategory newsCategory = newsCategoryDAO.getRow(id);
			if (newsCategory == null)
            {
                return HttpNotFound();
            }
			ViewBag.Listmenu = new SelectList(menuDAO.getList("Index"), "Id", "Name", 0);
			return View(newsCategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( NewsCategory newsCategory)
        {
            if (ModelState.IsValid)
            {
				newsCategory.Slug = XString.Str_Slug(newsCategory.Name);
				//newsCategory.UpdatedUserId = Convert.ToInt32(Session["UserId"].ToString());
				newsCategory.CreatedAt = DateTime.Now;
				newsCategory.UpdatedAt = DateTime.Now;
				if (newsCategoryDAO.Update(newsCategory) == 1)
				{
					Link link = linkDAO.getRow(newsCategory.Id, "NewsCategory");
					link.NameLink = newsCategory.Slug;
					linkDAO.Update(link);

				}
				TempData["message"] = new XMessage("success", "Cập nhật thành công!");
				return RedirectToAction("Index");
            }
			ViewBag.Listmenu = new SelectList(menuDAO.getList("Index"), "Id", "Name", 0);
			return View(newsCategory);
        }

        // GET: Admin/NewsCategory/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsCategory newsCategory = newsCategoryDAO.getRow(id);
			if (newsCategory == null)
            {
                return HttpNotFound();
            }
            return View(newsCategory);
        }

        // POST: Admin/NewsCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NewsCategory newsCategory = newsCategoryDAO.getRow(id);
			Link link = linkDAO.getRow(newsCategory.Id, "NewsCategory");
			if (newsCategoryDAO.Delete(newsCategory) == 1)
			{
				linkDAO.Delete(link);
			}
			TempData["message"] = new XMessage("success", "Xóa thành công!");
			return RedirectToAction("Index");
        }
    }
}
