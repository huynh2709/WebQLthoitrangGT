using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyClass.Models;

namespace MyClass.DAO
{
	public class OrderDAO
	{
		private DBContext db = new DBContext();

		//Trả về list Order
		/*public List<OrderInfo> getListJoin(string page = "All")
		{
			List<OrderInfo> list = null;
			switch (page)
			{
				case "Index":
					{
						list = db.Orders
							.Join(
								db.Orderdetails,
								o => o.Id,
								od => od.OrderID,
								(o,od) => new OrderInfo
								{
									 Id = o.Id,
									 IdCustomer = o.IdCustomer, 
									 NameCustomer  = o.NameCustomer,
									 IdProduct = o.IdProduct,
									 NameProduct = o.NameProduct,
									 Amount = o.Amount,
									 Note = o.Note,
									 Status = o.Status,
									 OrderID = od.OrderID,
									 ProductID = od.ProductID,
									 Price  = od.Price,
									 quantity = od.quantity
	}
							)
							.Where(m => m.Status != 0).ToList();
						break;
					}
				case "Trash":
					{
						list = db.Orders
							.Join(
								db.Orderdetails,
								o => o.Id,
								od => od.OrderID,
								(o, od) => new OrderInfo
								{
									Id = o.Id,
									IdCustomer = o.IdCustomer,
									NameCustomer = o.NameCustomer,
									IdProduct = o.IdProduct,
									NameProduct = o.NameProduct,
									Amount = o.Amount,
									Note = o.Note,
									Status = o.Status,
									OrderID = od.OrderID,
									ProductID = od.ProductID,
									Price = od.Price,
									quantity = od.quantity
								}
							)
							.Where(m => m.Status == 0).ToList();
						break;
					}
				default:
					{
						list = db.Orders
							.Join(
								db.Orderdetails,
								o => o.Id,
								od => od.OrderID,
								(o, od) => new OrderInfo
								{
									Id = o.Id,
									IdCustomer = o.IdCustomer,
									NameCustomer = o.NameCustomer,
									IdProduct = o.IdProduct,
									NameProduct = o.NameProduct,
									Amount = o.Amount,
									Note = o.Note,
									Status = o.Status,
									OrderID = od.OrderID,
									ProductID = od.ProductID,
									Price = od.Price,
									quantity = od.quantity
								}
							)
							.ToList();
						break;
					}
			}
			return list;
		}*/


		//Trả về list Order
		public List<Order> getList(string status="All")
		{
			return db.Orders.ToList();
		}
		
		//Trả về 1 Order
		public Order getRow(int? id)
		{
			if(id == null)
			{
				return null;
			}	
			else
			{
				return db.Orders.Find(id);
			}	
		}
		//Thêm Order
		public int Inset(Order row)
		{
			db.Orders.Add(row);
			return db.SaveChanges();
		}
		//Cập nhật Order
		public int Update(Order row)
		{
			db.Entry(row).State = EntityState.Modified;
			return db.SaveChanges();
		}
		//Cập nhật Order
		public int Delete(Order row)
		{
			db.Orders.Remove(row);
			return db.SaveChanges();
		}


	}
}
