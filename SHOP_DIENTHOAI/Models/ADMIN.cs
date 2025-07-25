using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SHOP_DIENTHOAI.Models
{
    [Table("ADMIN")]
    public class ADMIN
    {
        [Key]
        public int AdminId { get; set; }

        [Required(ErrorMessage = "Tên admin bắt buộc nhập")]
        [StringLength(50)]
        public string TEN_ADMIN { get; set; }

        [Required(ErrorMessage = "Mật khẩu bắt buộc nhập")]
        [StringLength(60)] // Mật khẩu đã được mã hóa với BCrypt có độ dài 60 ký tự
        public string MATKHAU { get; set; }

        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email sai định dạng")]
        [StringLength(50)]
        public string EMAIL { get; set; }

        [Required(ErrorMessage = "Số điện thoại bắt buộc nhập")]
        [StringLength(10, ErrorMessage = "Số điện thoại phải có 10 chữ số")]
        public string DIEN_THOAI { get; set; }

        [Required(ErrorMessage = "Địa chỉ bắt buộc nhập")]
        public string DIA_CHI { get; set; }

        // Liên kết với bảng PHAN_QUYEN
        public int? ID_Quyen { get; set; }

        public virtual PHAN_QUYEN PHAN_QUYEN { get; set; }
    }
}
