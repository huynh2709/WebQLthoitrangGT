using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;


namespace WebThoiTrangGioiTre
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
			//Database.SetInitializer<DbContext>(new DropCreateDatabaseIfModelChanges<DbContext>());
		}

		protected void Sesstion_Start()
		{
			//Lưu thông tin đăng nhập quản lý
			Session["UserAdmin"] = "";
			//Lưu id người đăng nhập quản lý
			Session["UserId"] = "1";
			//Giỏ hàng
			Session["giohang"] = "";
			//Lưu thông tin đăng nhập người dùng
			Session["UserCustomer"] = "";
		}
	}
}
