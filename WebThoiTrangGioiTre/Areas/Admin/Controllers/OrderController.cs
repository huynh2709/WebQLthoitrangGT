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

namespace WebThoiTrangGioiTre.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
		private OrderDAO orderDAO = new OrderDAO();
		private OrderdetailsDAO orderdetailsDAO = new OrderdetailsDAO();

        // GET: Admin/Order
        public ActionResult Index()
        {
			List<Order> list = orderDAO.getList("Index");
            return View(list);
        }

        // GET: Admin/Order/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = orderDAO.getRow(id);
			if (order == null)
            {
                return HttpNotFound();
            }
			ViewBag.ListChiTiet = orderdetailsDAO.getList(id);
            return View(order);
        }

        // GET: Admin/Order/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Order/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Order order)
        {
            if (ModelState.IsValid)
            {
				order.CreatedAt = DateTime.Now;
				orderDAO.Inset(order);
				return RedirectToAction("Index");
            }

            return View(order);
        }

        // GET: Admin/Order/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = orderDAO.getRow(id);
			if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Order order)
        {
            if (ModelState.IsValid)
            {
				order.CreatedAt = DateTime.Now;

				orderDAO.Update(order);
				return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: Admin/Order/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = orderDAO.getRow(id);
			if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Admin/Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = orderDAO.getRow(id);
			orderDAO.Delete(order);
			return RedirectToAction("Index");
        }

		public ActionResult Huy(int? id)
		{
			Order order = orderDAO.getRow(id);
			if(order == null)
			{
				return RedirectToAction("Index");

			}
			if(order.Status == 1 || order.Status == 2)
			{
				order.Status = 0;
				order.UpdateddAt = DateTime.Now;
			}
			else
			{
				if(order.Status == 3)
				{
					TempData["msg"] = new XMessage("danger", "Đơn hàng đang vận chuyển không thể hủy");
				}
				if (order.Status == 4)
				{
					TempData["msg"] = new XMessage("danger", "Đơn hàng đã thành công");
				}
				return RedirectToAction("Index");
			}
			orderDAO.Update(order);
			return RedirectToAction("Index");
		}
    }
}
