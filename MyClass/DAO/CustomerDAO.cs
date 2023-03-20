using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyClass.Models;

namespace MyClass.DAO
{
	public class CustomerDAO
	{
		private DBContext db = new DBContext();

		//Trả về list Customer
		public List<Customer> getList(string status="All")
		{
			return db.Customers.ToList();
		}
		
		//Trả về 1 Customer
		public Customer getRow(int? id)
		{
			if(id == null)
			{
				return null;
			}	
			else
			{
				return db.Customers.Find(id);
			}	
		}
		//Thêm Customer
		public int Inset(Customer row)
		{
			db.Customers.Add(row);
			return db.SaveChanges();
		}
		//Cập nhật Customer
		public int Update(Customer row)
		{
			db.Entry(row).State = EntityState.Modified;
			return db.SaveChanges();
		}
		//Cập nhật Customer
		public int Delete(Customer row)
		{
			db.Customers.Remove(row);
			return db.SaveChanges();
		}


	}
}
