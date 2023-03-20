using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyClass.DAO;
using MyClass.Models;
using PagedList;

namespace WebThoiTrangGioiTre.Controllers
{
    public class HomeController : Controller
    {
		LinkDAO linkDAO = new LinkDAO();	
		ProductDAO productDAO = new ProductDAO();
		ProductCategoryDAO productCategoryDAO = new ProductCategoryDAO();
		NewsDAO newsDAO = new NewsDAO();
		NewsCategoryDAO newsCategoryDAO = new NewsCategoryDAO();

        // Url mặc định hoặc url bất kỳ
        public ActionResult Index(string slug = null)
        {
			if(slug == null)
			{
				//Trang chủ
				return this.TrangChu();

			}	
			else
			{
				Link link = linkDAO.getRow(slug);
				if(link != null)
				{
					string typelink = link.Type;
					switch(typelink)
					{
						case "ProductCategory":
							{
								return this.ProductCategory(slug);
							}
						
						case "NewsCategory":
							{
								return this.NewsCategory(slug);
							}
						case "Page":
							{
								return this.Page(slug);
							}
						default:
							{
								return this.Error404(slug);
							}
					}	
				}
				else
				{
					Product product = productDAO.getRow(slug);
					if(product != null)
					{
						
						return this.ProductDetail(product);
					}
					else
					{
						News news = newsDAO.getRow(slug,"Page");
						if(news != null)
						{
							return this.NewsDetail(news);
						}
						else
						{
							
							return this.Error404(slug);
						}
					}
				}	
			}	
        }
		//trang chủ
		public ActionResult TrangChu()
		{
			List<ProductCategory> list = productCategoryDAO.getList();
			return View("TrangChu",list);
		}

		public ActionResult HomeProduct(int id)
		{
			ProductCategory productCategory = productCategoryDAO.getRow(id);
			ViewBag.ProductCategory = productCategory;

			//sp theo danh mucj loaij
			List<int> listcateid = new List<int>();
			listcateid.Add(productCategory.Id); //cap 1
			List<Product> list = productDAO.getListListByCateId(listcateid, 4);
			return View("HomeProduct", list);
		}
		//Nhóm action liên quan đến sp
		public ActionResult ProductCategory(string slug)
		{
			//Lấy PC dựa vào slug
			ProductCategory productCategory = productCategoryDAO.getRow(slug);
			ViewBag.ProductCategory = productCategory;
			List<int> listcateid = new List<int>();
			listcateid.Add(productCategory.Id); //cap 1
			List<Product> list = productDAO.getListListByCateId(listcateid, 10);
			return View("ProductCategory", list);


		}

		public ActionResult Product(int? page)//int pageIndex = 1, int pageSize = 2
		{
			int pageNumber = page ?? 1;//trang hiện tại
			int pageSize = 12;// số mẫu tin hiển thị trên 1 trang

			IPagedList<Product> list = productDAO.getListpage(pageSize, pageNumber);
			return View("Product",list);
		}
		
		public ActionResult PrDetail(int Id)
		{
			Product product = productDAO.getRowDetail(Id);
			ViewBag.ListOther = productDAO.getListByIdPrCate(product.IdCategory, product.Id);
			return View(product);
		}

		public ActionResult NewsCategory(string slug)
		{
			//Lấy PC dựa vào slug
			NewsCategory newsCategory = newsCategoryDAO.getRow(slug);
			ViewBag.newsCategory = newsCategory;
			List<int> listcateid = new List<int>();
			listcateid.Add(newsCategory.Id); //cap 1
			List<News> list = newsDAO.getListListByCateId(listcateid, 10);
			return View("NewsCategory",list);
		}

		public ActionResult News()
		{
			List<News> list = newsDAO.getList("News");
			return View("News",list);
		}

		public ActionResult NDetail(int id)
		{
			
			News news = newsDAO.getRowDetail(id);
			ViewBag.ListOther= newsDAO.getListByIdNewsCate(news.IdNewsCategory, "News", news.Id);
			return View(news);
		}

		
		public ActionResult Page(string slug)
		{
			News news = newsDAO.getRow(slug,"Page");
			return View("Page", news);
		}

		public ActionResult Error404(string slug)
		{

			return View("Error404");
		}

		/////
		public ActionResult ProductDetail(Product product)
		{
			return View("ProductDetail", product);
		}
		public ActionResult NewsDetail(News news)
		{
			return View("NewsDetail",news);
		}
	}
}