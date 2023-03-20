using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
	public class OrderInfo
	{
		public int Id { get; set; }
		public int IdCustomer { get; set; }
		[StringLength(200)]
		[DisplayName("Tên khách hàng")]
		public string NameCustomer { get; set; }
		public int IdProduct { get; set; }
		[StringLength(200)]
		[DisplayName("Tên sản phẩm")]
		public string NameProduct { get; set; }
		[DisplayName("Số lượng")]
		public string Amount { get; set; }//số lượng
		[DisplayName("Note")]
		public string Note { get; set; }
		[DisplayName("Trạng thái")]
		public int Status { get; set; }
		public int OrderID { get; set; }
		public int ProductID { get; set; }
		public int Price { get; set; }
		public int quantity { get; set; }// soos luong
		public DateTime? CreatedAt { get; set; }
	}
}
