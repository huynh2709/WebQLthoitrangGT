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
	[Table("Order")]
	public class Order
	{
		[Key]
		public int Id { get; set; }
		public int IdCustomer { get; set; }
		[StringLength(200)]
		[DisplayName("Tên khách hàng")]
		public string NameCustomer { get; set; }
		public string Email { get; set; }
		public string Address { get; set; }
		public string Phone { get; set; }
		public string Note { get; set; }
		public int Status { get; set; }
		public DateTime? CreatedAt { get; set; }
		public DateTime? UpdateddAt { get; set; }

	}
}
