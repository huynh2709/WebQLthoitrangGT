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
	[Table("ProductCategory")]
	public class ProductCategory
	{
		[Key]
		public int Id {get; set;}
		[StringLength(200)]
		[DisplayName("Tên danh mục")]
		[Required(ErrorMessage = "{0} là bắt buộc")]
		[MinLength(3, ErrorMessage = "{0} không được ít hơn 3 kí tự")]
		public string Name { get; set; }
		public string Slug { get; set; }
		public int? ParentId { get; set; }
		public string ParentName { get; set; }
		public DateTime? CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }
		public string NameCategory { get; internal set; }
	}
}
