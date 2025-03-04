using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SHOP_DIENTHOAI.Models
{
    public partial class CHI_TIET_DON_HANG
    {
        [Key]
        [Column(Order = 0)]
        public int MA_DON { get; set; }

        [Key]
        [Column(Order = 1)]
        public int MA_SP { get; set; }

        public int? SO_LUONG { get; set; }

        public int? DON_GIA { get; set; }

        public int? THANH_TIEN { get; set; }

        // PHUONGTHUC_THANHTOAN: 1 - Tiền mặt; 2 - Chuyển khoản
        public int? PHUONGTHUC_THANHTOAN { get; set; }

        public virtual DON_HANG DON_HANG { get; set; }

        public virtual SAN_PHAM SAN_PHAM { get; set; }
    }
}
