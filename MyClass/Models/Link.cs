using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
	public class Link
	{
		[Key]
		public int Id { get; set; }
		public string NameLink { get; set; }
		public int TableId { get; set; }
		public string Type { get; set; }

	}
}
