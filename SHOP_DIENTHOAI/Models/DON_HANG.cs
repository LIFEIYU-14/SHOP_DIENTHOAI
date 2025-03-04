using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SHOP_DIENTHOAI.Models
{
    public class DON_HANG
    {
        [Key]
        public int MA_DON { get; set; }

        public DateTime? NGAY_DAT { get; set; }

        // TINH_TRANG: 0 - Chờ xác nhận (tiền mặt); 1 - Đã xác nhận (tự động khi chuyển khoản hoặc admin xác nhận)
        public int? TINH_TRANG { get; set; }

        public int? MA_ND { get; set; }

        // THANHTOAN: 1 - Thanh toán tiền mặt; 2 - Thanh toán chuyển khoản
        public int? THANHTOAN { get; set; }

        [Required(ErrorMessage = "Địa chỉ nhận hàng bắt buộc nhập")]
        public string DIACHI_NHANHANG { get; set; }

        [Required(ErrorMessage = "Số điện thoại nhận hàng bắt buộc nhập")]
        [StringLength(10, ErrorMessage = "Số điện thoại phải có 10 chữ số")]
        public string SDT_NHANHANG { get; set; }
        public int? TONG_TIEN { get; set; }

        public virtual ICollection<CHI_TIET_DON_HANG> CHI_TIET_DON_HANG { get; set; }

        public virtual NGUOI_DUNG NGUOI_DUNG { get; set; }

    }

}
