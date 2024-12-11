using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BTL_CharityWebsite.ViewModel
{
    public class LienHeVM
    {
        [Required(ErrorMessage = "Họ và tên không được để trống.")]
        public string TenLH { get; set; }
        [Required(ErrorMessage = "Số điện thoại không được để trống.")]
        [RegularExpression(@"^[0-9]{10,15}$", ErrorMessage = "Số điện thoại không hợp lệ.")]
        public string SDTLH { get; set; }
        [Required(ErrorMessage = "Email không được để trống.")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng.")]
        public string EmailLH {  get; set; }
        public DateTime NgayGui {  get; set; }  = DateTime.Now;
    }
}