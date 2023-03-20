using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
	[Table("Product")]
	public class Product
	{
		[Key]
		public int Id { get; set; }
		[Required(ErrorMessage = "{0} là bắt buộc")]
		[MinLength(3, ErrorMessage = "{0} không được ít hơn 3 kí tự")]
		[DisplayName("Tên sản phẩm")]
		public string Name { get; set; }
		[DisplayName("Mô tả ngắn")]
		public string ShortDescription { get; set; }
		[DisplayName("Mô tả")]
		public string Description { get; set; }// mo ta
		[DisplayName("Giá")]
		public decimal Price { get; set; }

		[Required(AllowEmptyStrings = true)]
		[DisplayName("Khuyến mại")]
		public decimal Sale { get; set; }
		[DisplayName("Kích thước sản phẩm")]
		public string Size { get; set; }
		[DisplayName("Màu sắc")]
		public string Color { get; set; }
		public string Slug { get; set; }
		[DisplayName("Loại sản phẩm ")]
		public string TypeProduct { get; set; }
		[DisplayName("Số lượng sản phẩm")]
		public int Amount { get; set; } //số lượng
		[DisplayName("Hình ảnh")]
		public string Image { get; set; }
		public int IdCategory { get; set; }
		public string NameCategory { get; set; }
		public DateTime? CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }
	}
}
