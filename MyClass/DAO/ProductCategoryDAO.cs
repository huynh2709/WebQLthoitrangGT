using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyClass.Models;

namespace MyClass.DAO
{
	public class ProductCategoryDAO
	{
		private DBContext db = new DBContext();

		//Trả về list productcategory
		public List<ProductCategory> getList(string status = "All")
		{
			return db.ProductCategorys.ToList();
		}

		//---------------------------------------------
		public ProductCategory ViewDetail(int id)
		{
			return db.ProductCategorys.Find(id);
		}

		//------------------------------------------

		//Trả về list productcategory
		public List<ProductCategory> getListByParentId(int cateid)
		{
			return db.ProductCategorys.Where(m => m.ParentId == cateid).ToList();
		}

		//Trả về 1 productcategory
		public ProductCategory getRow(int? id)
		{
			if(id == null)
			{
				return null;
			}	
			else
			{
				return db.ProductCategorys.Find(id);
			}	
		}

		public ProductCategory getRow(string slug)
		{
				return db.ProductCategorys.Where(x => x.Slug == slug ).FirstOrDefault();	
		}

		//Thêm productcategory
		public int Inset(ProductCategory row)
		{
			db.ProductCategorys.Add(row);
			return db.SaveChanges();
		}
		//Cập nhật productcategory
		public int Update(ProductCategory row)
		{
			db.Entry(row).State = EntityState.Modified;
			return db.SaveChanges();
		}
		//Cập nhật productcategory
		public int Delete(ProductCategory row)
		{
			db.ProductCategorys.Remove(row);
			return db.SaveChanges();
		}


	}
}
