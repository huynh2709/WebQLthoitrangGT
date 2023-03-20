using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
	[Table("Slider")]
	public class Slider
	{
		[Key]
		public int Id { get; set; }
		public string Name { get; set; }
		public string Url { get; set; }
		public string Image { get; set; }
		public string Position { get; set; }
		public DateTime? CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }

	}
}
