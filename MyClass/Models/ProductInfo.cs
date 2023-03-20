using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
	public class ProductInfo
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }// mo ta
		public string Price { get; set; }
		public string Sale { get; set; }
		public string Size { get; set; }
		public string Color { get; set; }
		public string Slug { get; set; }
		public string TypeProduct { get; set; }
		public int Amount { get; set; } //số lượng
		public string Image { get; set; }
		public int IdCategory { get; set; }
		public string NameCategory { get; set; }
		public DateTime? CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }
		public int? CreateUserId { get; set; }
		public int? UpdatedUserId { get; set; }
	}
}
