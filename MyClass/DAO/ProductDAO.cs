using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyClass.Models;
using PagedList;

namespace MyClass.DAO
{
	public class ProductDAO
	{
		private DBContext db = new DBContext();

		//Trả về list Product
		public List<Product> getListListByCateId(List<int> listcateid, int take)
		{
			return db.Products
				.Where(m => listcateid.Contains(m.IdCategory))
				.OrderByDescending(m =>m.CreatedAt)
				.Take(take)
				.ToList();
		}

		public List<Product> getList(string all)
		{

			return db.Products.ToList();
		}

		
		public IPagedList<Product> getListpage( int pageSize, int pageNumber)
		{
			return db.Products
				.Where(m => m.IdCategory != 0)
				.OrderByDescending(m => m.CreatedAt)
			//	.Take(pageSize)
				.ToPagedList(pageNumber, pageSize);
		}

		public IPagedList<Product> getListpage(int id, int pageSize, int pageNumber)
		{
			return db.Products
				.Where(m => m.Id == id)
				.OrderByDescending(m => m.CreatedAt)
				//	.Take(pageSize)
				.ToPagedList(pageNumber, pageSize);
		}

		public List<Product> getListByIdPrCate(int? idnewscate, int? notid = null)
		{
			List<Product> list = null;
			if (notid == null)
			{
				list = db.Products.Where(n => n.IdCategory == idnewscate ).ToList();
			}
			else
			{
				list = db.Products.Where(n => n.IdCategory == idnewscate && n.Id != notid).ToList();
			}
			return list;
		}

		//Trả về 1 Product
		public Product getRow(int? id)
		{
			if(id == null)
			{
				return null;
			}	
			else
			{
				return db.Products.Find(id);
			}	
		}

		public Product getRowcart(int id)
		{
			if (id == null)
			{
				return null;
			}
			else
			{
				return db.Products.Find(id);
			}
		}

		public Product getRow(string slug)
		{
			return db.Products.Where(p => p.Slug == slug).FirstOrDefault();
			
		}
		public Product getRowDetail(int id )
		{
			return db.Products.Where(p => p.Id  == id).FirstOrDefault();

		}

		//Thêm Product
		public int Inset(Product row)
		{
			db.Products.Add(row);
			return db.SaveChanges();
		}
		//Cập nhật Product
		public int Update(Product row)
		{
			db.Entry(row).State = EntityState.Modified;
			return db.SaveChanges();
		}
		//Cập nhật Product
		public int Delete(Product row)
		{
			db.Products.Remove(row);
			return db.SaveChanges();
		}

		public List<Product> getListSlug(string slug)
		{
			return db.Products.
				Where(m => m.Slug == slug)
				.OrderByDescending(m => m.CreatedAt)
				.ToList();
		}
	}
}
