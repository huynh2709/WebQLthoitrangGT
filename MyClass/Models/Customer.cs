﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
	[Table("Customer")]
	public class Customer
	{
		[Key]
		public int Id { get; set; }
		[StringLength(200)]
		[DisplayName("Tên khách hàng")]
		[Required(ErrorMessage = "{0} là bắt buộc")]
		[MinLength(3, ErrorMessage = "{0} không được ít hơn 3 kí tự")]
		public string Name { get; set; }
		[DisplayName("Số điện thoại")]
		public string Phonenumber { get; set; }
		[DisplayName("Địa chỉ")]
		public string Address { get; set; }
		[DisplayName("Địa chỉ email")]
		public string Email { get; set; }
		[DisplayName("Mật khẩu")]
		public string PassWord { get; set; }
		public DateTime? CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }
		public int? CreateUserId { get; set; }
		public int? UpdatedUserId { get; set; }
	}
}
