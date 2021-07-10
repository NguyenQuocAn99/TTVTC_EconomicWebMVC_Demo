using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Validation;
namespace NguyenQuocAn.Models
{
    [MetadataTypeAttribute(typeof(ThanhVienMetadata))]
    public partial class ThanhVien
    {
        internal sealed class ThanhVienMetadata
        {
            public int MaThanhVien { get; set; }
            [Required(ErrorMessage ="username is not empty")]
            public string TaiKhoan { get; set; }
            [Required(ErrorMessage = "password is not empty")]
            public string MatKhau { get; set; }
            [Required(ErrorMessage = "Name is not empty")]
            public string HoTen { get; set; }
            public string DiaChi { get; set; }
            [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}")]
            public string Email { get; set; }
            [Required(ErrorMessage = "Phone is not empty")]
            [StringLength(12,ErrorMessage = "Phone must be {1} to {2}", MinimumLength = 8)]
            public string SoDienThoai { get; set; }
            public string CauHoi { get; set; }
            public string CauTraLoi { get; set; }
        }
    }
}