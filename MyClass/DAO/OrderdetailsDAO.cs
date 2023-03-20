using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyClass.Models;

namespace MyClass.DAO
{
	public class OrderdetailsDAO
	{
		private DBContext db = new DBContext();

		//Trả về list Order
		public List<Orderdetails> getList(int? orderId)
		{
			return db.Orderdetails.Where(x => x.OrderID == orderId).ToList();
		}
		
		//Trả về 1 Order
		public Orderdetails getRow(int? id)
		{
			if(id == null)
			{
				return null;
			}	
			else
			{
				return db.Orderdetails.Find(id);
			}	
		}
		//Thêm Order
		public int Inset(Orderdetails row)
		{
			db.Orderdetails.Add(row);
			return db.SaveChanges();
		}
		//Cập nhật Order
		public int Update(Orderdetails row)
		{
			db.Entry(row).State = EntityState.Modified;
			return db.SaveChanges();
		}
		//Cập nhật Order
		public int Delete(Orderdetails row)
		{
			db.Orderdetails.Remove(row);
			return db.SaveChanges();
		}


	}
}
