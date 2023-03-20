using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebThoiTrangGioiTre
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				name: "Login",
				url: "dang-nhap",
				defaults: new { controller = "Customer", action = "Login", id = UrlParameter.Optional }
			);
			

			routes.MapRoute(
				name: "Register",
				url: "dang-ky",
				defaults: new { controller = "Customer", action = "Register", id = UrlParameter.Optional }
			);

			routes.MapRoute(
				name: "Logout",
				url: "dang-xuat",
				defaults: new { controller = "Customer", action = "Logout", id = UrlParameter.Optional }
			);

			routes.MapRoute(
				name: "TatCaSanPham",
				url: "tat-ca-san-pham",
				defaults: new { controller = "Home", action = "Product", id = UrlParameter.Optional }
			);

			routes.MapRoute(
				name: "TatCaBaiViet",
				url: "tat-ca-bai-viet",
				defaults: new { controller = "Home", action = "News", id = UrlParameter.Optional }
			);

			routes.MapRoute(
				name: "LienHe",
				url: "lien-he",
				defaults: new { controller = "LienHe", action = "Contact", id = UrlParameter.Optional }
			);

			routes.MapRoute(
				name: "ThanhToan",
				url: "thanh-toan",
				defaults: new { controller = "GioHang", action = "ThanhToan", id = UrlParameter.Optional }
			);

			routes.MapRoute(
				name: "GioHang",
				url: "gio-hang",
				defaults: new { controller = "GioHang", action = "Index", id = UrlParameter.Optional }
			);
			routes.MapRoute(
				name: "Page",
				url: "tin-tuc",
				defaults: new { controller = "Home", action = "Page", id = UrlParameter.Optional }
			);

			routes.MapRoute(
				name: "TimKiem",
				url: "tim-kiem",
				defaults: new { controller = "TimKiem", action = "Index", id = UrlParameter.Optional }
			);

			//Khai báo url động - nằm kế trên Default
			routes.MapRoute(
				name: "HomeSlug",
				url: "{slug}",
				defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
			);

			routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
