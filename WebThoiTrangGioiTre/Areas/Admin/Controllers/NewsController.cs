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
	public class NewsController : Controller
	{
		private NewsDAO newsDAO = new NewsDAO();
		NewsCategoryDAO newsCategoryDAO = new NewsCategoryDAO();
		LinkDAO linkDAO = new LinkDAO();

		// GET: Admin/News
		public ActionResult Index()
		{
			return View(newsDAO.getList("Index","News"));
		}

		// GET: Admin/News/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			News newss = newsDAO.getRow(id);
			if (newss == null)
			{
				return HttpNotFound();
			}
			return View(newss);
		}

		// GET: Admin/News/Create
		public ActionResult Create()
		{
			ViewBag.ListCate = new SelectList(newsCategoryDAO.getList("Index"), "Id", "Name", 0);
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[ValidateInput(false)]
		public ActionResult Create(News newss)
		{
			if (ModelState.IsValid)
			{
				newss.Slug = XString.Str_Slug(newss.Name);
				if (newss.IdNewsCategory == null)
				{
					newss.IdNewsCategory = 0;
				}

				var img = Request.Files["img"];//lấy thông tin file
				if (img.ContentLength != 0)
				{
					string[] FileExtentions = new string[] { ".jpg", ".jepg", ".png", ".gif" };
					//kiểm tra file
					if (FileExtentions.Contains(img.FileName.Substring(img.FileName.LastIndexOf("."))))
					{
						//upload hình
						string imgName = newss.Slug + img.FileName.Substring(img.FileName.LastIndexOf("."));
						newss.Image = imgName;
						string PathDir = "~/Public/images/blog/";
						string PathFile = Path.Combine(Server.MapPath(PathDir), imgName);
						img.SaveAs(PathFile);
					}
				}

				//newss.CreateUserId = Convert.ToInt32(Session["UserId"].ToString());
				newss.TypeNews = "News";
				newss.CreatedAt = DateTime.Now;
				if (newsDAO.Inset(newss) == 1)
				{
					Link link = new Link();
					link.NameLink = newss.Slug;
					link.TableId = newss.Id;
					link.Type = "News";
					linkDAO.Inset(link);
				}
				TempData["message"] = new XMessage("success", "Thêm thành công!");
				return RedirectToAction("Index");
			}
			ViewBag.ListCate = new SelectList(newsCategoryDAO.getList("Index"), "Id", "Name", 0);
			return View(newss);
		}

		// GET: Admin/News/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			News newss = newsDAO.getRow(id);
			if (newss == null)
			{
				return HttpNotFound();
			}
			ViewBag.ListCate = new SelectList(newsCategoryDAO.getList("Index"), "Id", "Name", 0);
			return View(newss);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[ValidateInput(false)]
		public ActionResult Edit(News newss)
		{
			if (ModelState.IsValid)
			{
				newss.Slug = XString.Str_Slug(newss.Name);
				var img = Request.Files["img"];//lấy thông tin file
				if (img.ContentLength != 0)
				{
					string[] FileExtentions = new string[] { ".jpg", ".jepg", ".png", ".gif" };
					//kiểm tra file
					if (FileExtentions.Contains(img.FileName.Substring(img.FileName.LastIndexOf("."))))
					{
						//upload hình
						string imgName = newss.Slug + img.FileName.Substring(img.FileName.LastIndexOf("."));
						newss.Image = imgName;
						string PathDir = "~/Public/images/blog/";
						string PathFile = Path.Combine(Server.MapPath(PathDir), imgName);
						img.SaveAs(PathFile);
					}
				}

				//newss.UpdatedUserId = Convert.ToInt32(Session["UserId"].ToString());
				newss.TypeNews = "News";
				newss.CreatedAt = DateTime.Now;
				newss.UpdatedAt = DateTime.Now;
				if (newsDAO.Update(newss) == 1)
				{
					Link link = linkDAO.getRow(newss.Id, "News");
					link.NameLink = newss.Slug;
					linkDAO.Update(link);

				}
				TempData["message"] = new XMessage("success", "Cập nhật thành công!");
				return RedirectToAction("Index");
			}
			ViewBag.ListCate = new SelectList(newsCategoryDAO.getList("Index"), "Id", "Name", 0);
			return View(newss);
		}

		// GET: Admin/News/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			News newss = newsDAO.getRow(id);
			if (newss == null)
			{
				return HttpNotFound();
			}
			return View(newss);
		}

		// POST: Admin/News/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			News newss = newsDAO.getRow(id);
			Link link = linkDAO.getRow(newss.Id, "News");
			//Xóa hình ảnh
			string PathDir = "~/Public/images/news/";
			//Xóa file
			if (newss.Image != null)
			{
				string DelPath = Path.Combine(Server.MapPath(PathDir), newss.Image);
				System.IO.File.Delete(DelPath);
			}
			if (newsDAO.Delete(newss) == 1)
			{
				linkDAO.Delete(link);
			}
			return RedirectToAction("Index");
		}
	}
}
