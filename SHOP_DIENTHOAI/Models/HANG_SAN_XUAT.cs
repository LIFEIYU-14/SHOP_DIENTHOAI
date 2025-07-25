using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SHOP_DIENTHOAI.Models
{
    public class HANG_SAN_XUAT
    {
        [Key]
        public int MA_HSX { get; set; }

        [StringLength(50)]
        public string TEN_HSX { get; set; }
    }
}