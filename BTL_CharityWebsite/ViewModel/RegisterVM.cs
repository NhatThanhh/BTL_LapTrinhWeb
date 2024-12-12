using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BTL_CharityWebsite.ViewModel
{
    public class RegisterVM
    {
        [Required(ErrorMessage ="Họ tên không được để trống")]
        public string HoTen {  get; set; }
        [Required(ErrorMessage = "Tài khoản không được để trống")]
        public string TaiKhoan{ get; set; }
        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        public string MatKhau { get; set; }
        [Required(ErrorMessage = "Xác nhận mật khẩu không được để trống")]
        [Compare("MatKhau", ErrorMessage ="Xác nhận mật khẩu không chính xác")]
        public string XacNhanMK {  get; set; }
        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng.")]
        public string Email {  get; set; }
        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [RegularExpression(@"^[0-9]{10,15}$", ErrorMessage = "Số điện thoại không hợp lệ.")]
        public string SDT { get; set; }
        [Required(ErrorMessage = "Địa chỉ không được để trống")]
        public string DiaChi { get; set; }
        [Required(ErrorMessage = "Ngày sinh không được để trống")]
        public DateTime? NgaySinh { get; set; }
    }
}