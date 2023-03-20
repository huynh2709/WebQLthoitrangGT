using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyClass.Models;

namespace MyClass.DAO
{
	public class NewsCategoryDAO
	{
		private DBContext db = new DBContext();

		//Trả về list NewsCategory
		public List<NewsCategory> getList(string status="All")
		{
			return db.NewsCategorys.ToList();
		}

		//Trả về 1 NewsCategory
		public NewsCategory getRow(int? id)
		{
			if(id == null)
			{
				return null;
			}	
			else
			{
				return db.NewsCategorys.Find(id);
			}	
		}

		public NewsCategory getRow(string slug)
		{
			return db.NewsCategorys.Where(x => x.Slug == slug).FirstOrDefault();
		}

		//Thêm NewsCategory
		public int Inset(NewsCategory row)
		{
			db.NewsCategorys.Add(row);
			return db.SaveChanges();
		}
		//Cập nhật NewsCategory
		public int Update(NewsCategory row)
		{
			db.Entry(row).State = EntityState.Modified;
			return db.SaveChanges();
		}
		//xóa NewsCategory
		public int Delete(NewsCategory row)
		{
			db.NewsCategorys.Remove(row);
			return db.SaveChanges();
		}


	}
}
