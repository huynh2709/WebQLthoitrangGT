using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebThoiTrangGioiTre.Library;
using MyClass.DAO;
using MyClass.Models;
using System.Security.Cryptography;
using System.Text;

namespace WebThoiTrangGioiTre.Controllers
{
	public class CustomerController : Controller
	{
		private DBContext _db = new DBContext();
		UserDAO userDAO = new UserDAO();
		//
		// GET: Customer
		public ActionResult Index()
		{
			if (Session["idUser"] != null)
			{
				return View();
			}
			else
			{
				return RedirectToAction("Login");
			}
		}

		//GET: Register

		public ActionResult Register()
		{
			return View();
		}

		//POST: Register
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Register(User _user)
		{
			if (ModelState.IsValid)
			{
				var check = _db.Users.FirstOrDefault(s => s.Email == _user.Email);
				if (check == null)
				{
					_user.Password = GetMD5((string)_user.Password);
					_user.Roles = "user";
					_user.CreatedAt = DateTime.Now;
					_db.Configuration.ValidateOnSaveEnabled = false;
					_db.Users.Add(_user);
					_db.SaveChanges();
					return RedirectToAction("Index", "Home");
				}
				else
				{
					ViewBag.error = "Email already exists";
					return View();
				}
			}
			return View();
		}

		public ActionResult Login()
		{
			return View();
		}



		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Login(string email, string password)
		{   
			if (ModelState.IsValid)
			{
				var user = new User();
				var f_password = GetMD5(password);
				var data = _db.Users.Where(s => s.Email.Equals(email) && s.Password.Equals(f_password)).ToList();
				if (data.Count() > 0)
				{
					//add session
					//Session["FullName"] = data.FirstOrDefault().FirstName + " " + data.FirstOrDefault().LastName;
					Session["Email"] = data.FirstOrDefault().Email;
					Session["idUser"] = data.FirstOrDefault().idUser;
					if (Session["idUser"] != null)
					{
						if (data[0].Roles == "admin")
						{
							return Redirect("~/admin");						
						}
						else if (data[0].Roles == "user")
						{
							return RedirectToAction("Index", "GioHang");
						}
					}
				}
				else
				{
					return RedirectToAction("Login");
				}
			}
			return View();
		}


		//Logout
		public ActionResult Logout()
		{
			Session.Clear();
			return RedirectToAction("Index", "Home");
		}



		//create a string MD5
		public static string GetMD5(string str)
		{
			MD5 md5 = new MD5CryptoServiceProvider();
			byte[] fromData = Encoding.UTF8.GetBytes(str);
			byte[] targetData = md5.ComputeHash(fromData);
			string byte2String = null;

			for (int i = 0; i < targetData.Length; i++)
			{
				byte2String += targetData[i].ToString("x2");

			}
			return byte2String;
		}

	}

}