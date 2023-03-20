using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
	[Table("Menus")]
	public class Menu
	{
		[Key]
		public int? Id { get; set; }
		public string Name { get; set; }
		public int? TableId { get; set; }
		public string TypeMenu { get; set; }
		public int? ParentId { get; set; }
		public string Link { get; set; }
		public string Position { get; set; }//vị trí menu
		public DateTime? CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }

	}
}
