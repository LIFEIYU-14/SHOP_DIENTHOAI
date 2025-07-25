using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SHOP_DIENTHOAI.Models
{
    public class NGUOI_DUNG
    {
        [Key]
        public int MA_ND { get; set; }

        [Required(ErrorMessage = "Họ tên bắt buộc nhập")]
        [StringLength(50)]
        public string TEN_ND { get; set; }

        [Required(ErrorMessage = "Mật khẩu bắt buộc nhập")]
        [StringLength(60)] // Độ dài mật khẩu mã hóa luôn là 60 ký tự
        public string MATKHAU { get; set; }  // Không cho phép thay đổi trực tiếp từ bên ngoài

        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email sai định dạng")]
        [StringLength(50)]
        public string EMAIL { get; set; }

        [Required(ErrorMessage = "Điện thoại bắt buộc nhập")]
        [StringLength(10, ErrorMessage = "Số điện thoại phải có 10 chữ số")]
        public string DIEN_THOAI { get; set; }


        public int? ID_Quyen { get; set; }

        [Required(ErrorMessage = "Địa chỉ bắt buộc nhập")]
        public string DIA_CHI { get; set; }

        public virtual ICollection<DON_HANG> DON_HANG { get; set; }
        public virtual PHAN_QUYEN PHAN_QUYEN { get; set; }
      
    }
    public static class PasswordHelper
    {
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }

}
