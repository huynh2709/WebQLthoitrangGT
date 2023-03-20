using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
	public class DBContext : DbContext
	{
		public DBContext() : base("name=StrConnect")
		{

		}
		public DbSet<Product> Products { get; set; }
		public DbSet<ProductCategory> ProductCategorys { get; set; }
		public DbSet<News> Newss { get; set; }
		public DbSet<NewsCategory> NewsCategorys { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<Orderdetails> Orderdetails { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Slider> Sliders { get; set; }
		public DbSet<Menu> Menus { get; set; }
		public DbSet<Link> Links { get; set; }
	}

}
