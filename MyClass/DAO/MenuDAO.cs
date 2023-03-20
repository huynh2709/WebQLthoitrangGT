using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using MyClass.Models;

namespace MyClass.DAO
{
	public class MenuDAO
	{
		private DBContext db = new DBContext();

		//Trả về list Menus
		public List<Menu> getListByParentId(string position, int parentid=0 )
		{
			return db.Menus.Where(m=>m.ParentId == parentid && m.Position == position).ToList();
		}

		public List<Menu> getList(string list)
		{
			return db.Menus.ToList();
		}

		//Trả về 1 Menu
		public Menu getRow(int? id)
		{
			if (id == null)
			{
				return null;
			}
			else
			{
				return db.Menus.Find(id);
			}
		}
		//Thêm Menu
		public int Insert(Menu row)
		{
			db.Menus.Add(row);
			return db.SaveChanges();
		}
		//Cập nhật Menu
		public int Update(Menu row)
		{
			db.Entry(row).State = EntityState.Modified;
			return db.SaveChanges();
		}
		//Cập nhật Menu
		public int Delete(Menu row)
		{
			db.Menus.Remove(row);
			return db.SaveChanges();
		}

	}
}
