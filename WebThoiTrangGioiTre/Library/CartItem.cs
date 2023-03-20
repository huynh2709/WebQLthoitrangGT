using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebThoiTrangGioiTre
{
	public class CartItem
	{
		public int SanPhamID { get; set; }
		public string Hinh { get; set; }
		public string TenSanPham { get; set; }
		public int Gia { get; set; }
		public int Giam { get; set; }
		public int DonGia { get {
				return ThanhTien + 25000;
					} }
		public int SoLuong { get; set; }
		public int ThanhTien
		{
			get
			{
				return SoLuong * Giam;
			}
		}
	}
}