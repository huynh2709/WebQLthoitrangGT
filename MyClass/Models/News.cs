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
	[Table("News")]
	public class News
	{
		[Key]
		public int Id { get; set; }
		[Required(ErrorMessage = "{0} là bắt buộc")]
		[MinLength(3, ErrorMessage = "{0} không được ít hơn 3 kí tự")]
		[DisplayName("Tên Tin Tức")]
		public string Name { get; set; }	
		[DisplayName("Nội dung")]
		public string Content { get; set; }
		[DisplayName("Hình ảnh")]
		public string Image { get; set; }
		public string Slug { get; set; }
		public string TypeNews { get; set; }
		public int? ParentId { get; set; }
		public string ParentNam { get; set; }
		public int? IdNewsCategory { get; set; }
		public string NameNewsCategory { get; set; }
		public DateTime? CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }

	}
}
