using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SHOP_DIENTHOAI.Models
{
    public class SAN_PHAM
    {
        [Key]
        public int MA_SP { get; set; }
        public string TEN_SP { get; set; }
        public int? MA_HSX { get; set; }
        public int? GIA { get; set; }
        public int? SOLUONG { get; set; }
        public string GHICHU_SP { get; set; }
        public string HinhAnh { get; set; }
        public string Hinh1 { get; set; }
        public string Hinh2 { get; set; }
        public string Hinh3 { get; set; }
        public string Hinh4 { get; set; }
        public virtual ICollection<CHI_TIET_DON_HANG> CHI_TIET_DON_HANG { get; set; }
        public virtual HANG_SAN_XUAT HANG_SAN_XUAT { get; set; }
        public virtual ICollection<COMMENT> COMMENT { get; set; } = new List<COMMENT>();
    }
}
