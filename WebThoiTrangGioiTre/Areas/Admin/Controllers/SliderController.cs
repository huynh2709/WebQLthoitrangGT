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
	public class SliderController : Controller
	{
		SliderDAO sliderDAO = new SliderDAO();
		LinkDAO linkDAO = new LinkDAO();

		// GET: Admin/Slider
		public ActionResult Index()
		{
			return View(sliderDAO.getList("Index"));
		}

		// GET: Admin/Slider/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Slider slider = sliderDAO.getRow(id);
			if (slider == null)
			{
				return HttpNotFound();
			}
			return View(slider);
		}

		// GET: Admin/Slider/Create
		public ActionResult Create()
		{
			return View();
		}

		
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(Slider slider)
		{
			if (ModelState.IsValid)
			{
				slider.Url = XString.Str_Slug(slider.Name);
				var img = Request.Files["img"];//lấy thông tin file
				if (img.ContentLength != 0)
				{
					string[] FileExtentions = new string[] { ".jpg", ".jepg", ".png", ".gif" ,".webp"};
					//kiểm tra file
					if (FileExtentions.Contains(img.FileName.Substring(img.FileName.LastIndexOf("."))))
					{
						//upload hình
						string imgName = slider.Name + img.FileName.Substring(img.FileName.LastIndexOf("."));
						slider.Image = imgName;
						string PathDir = "~/Public/images/sliders/";
						string PathFile = Path.Combine(Server.MapPath(PathDir), imgName);
						img.SaveAs(PathFile);
					}
				}
				slider.Position = "Slideshow";
				slider.CreatedAt = DateTime.Now;
				if (sliderDAO.Inset(slider) == 1)
				{
					Link link = new Link();
					link.NameLink = slider.Url;
					link.TableId = slider.Id;
					link.Type = "slider";
					linkDAO.Inset(link);
				}
				TempData["message"] = new XMessage("success", "Thêm thành công!");
				return RedirectToAction("Index","Slider");
			}

			return View(slider);
		}

		// GET: Admin/Slider/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Slider slider = sliderDAO.getRow(id);
			if (slider == null)
			{
				return HttpNotFound();
			}
			return View(slider);
		}

		
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(Slider slider)
		{
			if (ModelState.IsValid)
			{
				slider.Url = XString.Str_Slug(slider.Name);
				var img = Request.Files["img"];//lấy thông tin file
				if (img.ContentLength != 0)
				{
					string[] FileExtentions = new string[] { ".jpg", ".jepg", ".png", ".gif" ,".webp"};
					//kiểm tra file
					if (FileExtentions.Contains(img.FileName.Substring(img.FileName.LastIndexOf("."))))
					{
						//upload hình
						string imgName = slider.Name + img.FileName.Substring(img.FileName.LastIndexOf("."));
						slider.Image = imgName;
						string PathDir = "~/Public/images/sliders/";
						string PathFile = Path.Combine(Server.MapPath(PathDir), imgName);

						//Xóa file
						if (slider.Image.Length > 0)
						{
							string DelPath = Path.Combine(Server.MapPath(PathDir), slider.Image);
							System.IO.File.Delete(DelPath);
						}

						img.SaveAs(PathFile);
					}
				}
				slider.Position = "Slideshow";
				slider.CreatedAt = DateTime.Now;
				slider.UpdatedAt = DateTime.Now;

				if (sliderDAO.Update(slider) == 1)
				{
					Link link = new Link();
					link.NameLink = slider.Url;
					link.TableId = slider.Id;
					link.Type = "slider";
					linkDAO.Update(link);
				}
				TempData["message"] = new XMessage("success", "Cập nhật thành công!");
				return RedirectToAction("Index");
			}
			return View(slider);
		}

		// GET: Admin/Slider/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Slider slider = sliderDAO.getRow(id);
			if (slider == null)
			{
				return HttpNotFound();
			}
			return View(slider);
		}

		// POST: Admin/Slider/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			Slider slider = sliderDAO.getRow(id);
			Link link = linkDAO.getRow(slider.Id, "slider");
			//Xóa hình ảnh
			string PathDir = "~/Public/images/sliders/";
			//Xóa file
			if (slider.Image != null)
			{
				string DelPath = Path.Combine(Server.MapPath(PathDir), slider.Image);
				System.IO.File.Delete(DelPath);
			}
			if (sliderDAO.Delete(slider) == 1)
			{
				linkDAO.Delete(link);
			}
			return RedirectToAction("Index");
		}
	}
}
