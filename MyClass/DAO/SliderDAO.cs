using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyClass.Models;

namespace MyClass.DAO
{
	public class SliderDAO
	{
		private DBContext db = new DBContext();

		public List<Slider> getListByPosition(string position)
		{
			return db.Sliders.Where(s => s.Position == position).ToList();
		}

		//Trả về list Slider
		public List<Slider> getList(string status="All")
		{
			return db.Sliders.ToList();
		}
		
		//Trả về 1 Slider
		public Slider getRow(int? id)
		{
			if(id == null)
			{
				return null;
			}	
			else
			{
				return db.Sliders.Find(id);
			}	
		}
		//Thêm Slider
		public int Inset(Slider row)
		{
			db.Sliders.Add(row);
			return db.SaveChanges();
		}
		//Cập nhật Slider
		public int Update(Slider row)
		{
			db.Entry(row).State = EntityState.Modified;
			return db.SaveChanges();
		}
		//Cập nhật Slider
		public int Delete(Slider row)
		{
			db.Sliders.Remove(row);
			return db.SaveChanges();
		}


	}
}
