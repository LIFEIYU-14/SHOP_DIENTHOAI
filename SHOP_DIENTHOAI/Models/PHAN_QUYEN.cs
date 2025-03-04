using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SHOP_DIENTHOAI.Models
{
    public class PHAN_QUYEN
    {
        [Key]
        public int ID_Quyen { get; set; }

     
        [Required(ErrorMessage = "Tên quyền không được để trống")]
        [StringLength(50, ErrorMessage = "Tên quyền không được quá 50 ký tự")]
        public string Ten_Quyen { get; set; }

        public virtual ICollection<NGUOI_DUNG> NGUOI_DUNG { get; set; } = new List<NGUOI_DUNG>();
        public virtual ICollection<ADMIN> ADMINs { get; set; } = new List<ADMIN>();

    }
}