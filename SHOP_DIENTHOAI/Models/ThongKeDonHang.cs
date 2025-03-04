using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SHOP_DIENTHOAI.Models
{
    public class ThongKeDonHang
    {
        public DateTime? Ngay { get; set; }
        public int TongSoDonHang { get; set; }
        public decimal TongDoanhThu { get; set; }
    }
}