using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyClass.Models;

namespace MyClass.DAO
{
	public class NewsDAO
	{
		private DBContext db = new DBContext();

		//Trả về list News
		public List<News> getList(string status = "All",string type = "News")
		{
			return db.Newss.Where(n =>n.TypeNews == type).ToList();
		}

		public List<News> getListByIdNewsCate(int? idnewscate, string type = "News", int? notid = null )
		{
			List<News> list = null;
			if(notid == null)
			{
				list = db.Newss.Where(n => n.IdNewsCategory == idnewscate && n.TypeNews == type).ToList();
			}
			else
			{
				list = db.Newss.Where(n => n.IdNewsCategory == idnewscate && n.TypeNews == type && n.Id != notid).ToList();
			}
			return list;
		}

		public List<News> getListListByCateId(List<int> listcateid, int take)
		{
			return db.Newss
				.Where(m => listcateid.Contains((int)m.IdNewsCategory))
				.OrderByDescending(m => m.CreatedAt)
				.Take(take)
				.ToList();
		}

		public News getRowDetail(int id)
		{
			return db.Newss.Where(n => n.Id == id).FirstOrDefault();
		}
		
		//Trả về 1 News
		public News getRow(int? id)
		{
			if(id == null)
			{
				return null;
			}	
			else
			{
				return db.Newss.Find(id);
			}	
		}

		
		public News getRow(string slug, string typenews)
		{
			return db.Newss.Where(n => n.TypeNews == typenews && n.Slug == slug).FirstOrDefault();

		}

		//Thêm News
		public int Inset(News row)
		{
			db.Newss.Add(row);
			return db.SaveChanges();
		}
		//Cập nhật News
		public int Update(News row)
		{
			db.Entry(row).State = EntityState.Modified;
			return db.SaveChanges();
		}
		//Cập nhật News
		public int Delete(News row)
		{
			db.Newss.Remove(row);
			return db.SaveChanges();
		}


	}
}
