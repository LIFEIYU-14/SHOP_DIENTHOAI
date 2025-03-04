using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SHOP_DIENTHOAI.Models
{
    public class COMMENT
    {
        [Key]
        public int MA_CMT { get; set; }

        [Required(ErrorMessage = "Nội dung bình luận không được để trống")]
        [StringLength(500, ErrorMessage = "Nội dung bình luận tối đa 500 ký tự")]
        public string NOIDUNG_CMT { get; set; }

        public DateTime NGAY_CMT { get; set; } = DateTime.Now;

        // Khóa ngoại liên kết với NGUOI_DUNG
        [Required]
        public int MA_ND { get; set; }

        [ForeignKey("MA_ND")]
        public virtual NGUOI_DUNG NGUOI_DUNG { get; set; }

        // Khóa ngoại liên kết với SAN_PHAM
        [Required]
        public int MA_SP { get; set; }

        [ForeignKey("MA_SP")]
        public virtual SAN_PHAM SAN_PHAM { get; set; }
    }
}
