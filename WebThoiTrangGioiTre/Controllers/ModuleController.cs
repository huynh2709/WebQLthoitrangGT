using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyClass.DAO;
using MyClass.Models;

namespace WebThoiTrangGioiTre.Controllers
{
	public class ModuleController : Controller
	{
		private MenuDAO menuDAO = new MenuDAO();
		private SliderDAO sliderDAO = new SliderDAO();
		private ProductCategoryDAO productCategoryDAO = new ProductCategoryDAO();
		private NewsCategoryDAO newsCategoryDAO = new NewsCategoryDAO();

		public ActionResult MainMenu()
		{
			List<Menu> list = menuDAO.getListByParentId("mainmenu", 0);
			return View("MainMenu", list);
		}

		public ActionResult MainMenuSub(int id)
		{
			Menu menu = menuDAO.getRow(id);
			List<Menu> list = menuDAO.getListByParentId("mainmenu", id);
			if (list.Count == 0)
			{
				//không có cấp con
				return View("MainMenuSub1", menu);
			}
			else
			{
				ViewBag.Menu = menu;
				//có con
				return View("MainMenuSub2", list);
			}
		}

		//Slideshow
		public ActionResult Slideshow()
		{
			List<Slider> list = sliderDAO.getListByPosition("Slideshow");
			return View("Slideshow", list);
		}
		//ListCategory
		public ActionResult ListCategory()
		{
			List<ProductCategory> list = productCategoryDAO.getList();
			return View("ListCategory", list);
		}

		public ActionResult ListNews()
		{
			List<NewsCategory> list = newsCategoryDAO.getList();
			return View("ListNews", list);
		}

		public ActionResult MenuFooter()
		{
			List<Menu> list = menuDAO.getListByParentId("footermenu", 0);
			return View("MenuFooter", list);
		}
	}
}