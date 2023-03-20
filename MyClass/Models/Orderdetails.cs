using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
	[Table("Orderdetails")]
	public class Orderdetails
	{
		[Key]
		public int Id { get; set; }
		public int OrderID { get; set; }
		public int ProductID { get; set; }
		public string NameProduct { get; set; }
		public int Price { get; set; }
		public int Amount { get; set; }
		public int quantity { get; set; }// soos luong
		public DateTime? CreatedAt { get; set; }

	}
}
