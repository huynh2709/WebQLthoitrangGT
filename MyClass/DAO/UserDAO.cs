using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyClass.Models;

namespace MyClass.DAO
{
	public class UserDAO
	{
		private DBContext db = new DBContext();

		//Trả về list User
		public List<User> getList(string status="All")
		{
			return db.Users.ToList();
		}
		
		//Trả về 1 User
		public User getRow(int? id)
		{
			if(id == null)
			{
				return null;
			}	
			else
			{
				return db.Users.Find(id);
			}	
		}

		/*public User getRow(string username,  string roles)
		{
				return db.Users
					.Where(u => u.Roles == roles && (u.Name == username || u.Email == username))
					.FirstOrDefault();
		}*/
		//Thêm User
		public int Inset(User row)
		{
			db.Users.Add(row);
			return db.SaveChanges();
		}
		//Cập nhật User
		public int Update(User row)
		{
			db.Entry(row).State = EntityState.Modified;
			return db.SaveChanges();
		}
		//Cập nhật User
		public int Delete(User row)
		{
			db.Users.Remove(row);
			return db.SaveChanges();
		}


	}
}
