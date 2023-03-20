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
	[Table("User")]
	public class User
	{

		[Key, Column(Order = 1)]
		[DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
		public int idUser { get; set; }

		[Required]
		[StringLength(50, MinimumLength = 3, ErrorMessage = "Tên người dùng phải có độ dài bắt buộc từ 3 - 59 kí tự")]
		public string FirstName { get; set; }

		

		[Required]
		[RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Email không hợp lệ")]
		public string Email { get; set; }

		[Required]
		[StringLength(20, MinimumLength = 6, ErrorMessage = "Mật khẩu phải có độ dài ngắn nhất là 6 kí tự")]
		//[MaxLength(20, ErrorMessage = "Mật khẩu phải có độ dài ngắn nhất là 4 kí tự")]
		//[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$")]
		public string Password { get; set; }

		[NotMapped]
		[Required]
		[System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Mật khẩu xác nhận không chính xác")]
		public string ConfirmPassword { get; set; }
		
		public string Phone { get; set; }
		public string Roles { get; set; }
		public string Address { get; set; }
		public string Note { get; set; }
		public DateTime? CreatedAt { get; set; }
	}
}
