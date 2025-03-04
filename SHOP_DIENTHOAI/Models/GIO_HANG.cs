using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SHOP_DIENTHOAI.Models
{
    public class GIO_HANG
    {
        ModelDienThoai dt = new ModelDienThoai();
        public int iMasp { get; set; }
        public string sTensp { get; set; }
        public string sAnhBia { get; set; }
        public double dDonGia { get; set; }
        public int iSoLuong { get; set; }
        public double ThanhTien
        {
            get { return iSoLuong * dDonGia; }
        }
        // Hàm tạo cho giỏ hàng
        public GIO_HANG(int Masp)
        {
            iMasp = Masp;
            SAN_PHAM sp = dt.SAN_PHAM.Single(n => n.MA_SP == iMasp);
            sTensp = sp.TEN_SP;
            sAnhBia = sp.HinhAnh;
            dDonGia = double.Parse(sp.GIA.ToString());
            iSoLuong = 1;
        }
    }
}
