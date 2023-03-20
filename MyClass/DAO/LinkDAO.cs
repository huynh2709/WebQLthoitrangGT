using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
	public class LinkDAO
	{
		private DBContext db = new DBContext();

		//Trả về list Cart
		public List<Link> getList(string status = "All")
		{
			return db.Links.ToList();
		}

		public Link getRow(int? id)
		{
			return db.Links.Find(id);		
		}

		public Link getRow(string slug)
		{
			return db.Links.Where(l => l.NameLink ==slug).FirstOrDefault();
		}

		//Trả về 1 Cart
		public Link getRow(int tableid, string typelink)
		{
			return db.Links.Where(l => l.TableId == tableid && l.Type == typelink).FirstOrDefault();
			
		}
		//Thêm Cart
		public int Inset(Link row)
		{
			db.Links.Add(row);
			return db.SaveChanges();
		}
		//Cập nhật Cart
		public int Update(Link row)
		{
			db.Entry(row).State = EntityState.Modified;
			return db.SaveChanges();
		}
		//Cập nhật Cart
		public int Delete(Link row)
		{
			db.Links.Remove(row);
			return db.SaveChanges();
		}
	}
}
