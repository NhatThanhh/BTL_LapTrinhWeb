﻿using BTL_CharityWebsite.Models;
using BTL_CharityWebsite.ViewModel;
using PagedList;
using PagedList.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTL_CharityWebsite.Areas.Admin
{
    public class UsersController : Controller
    {
        private readonly CharityManagementEntities db;
        public UsersController(CharityManagementEntities db)
        {
            this.db = db;
        }
        public UsersController() : this(new CharityManagementEntities()) { }
        // GET: Admin/Users
        public ActionResult Index()
        {
            return View();
        }
        public List<NGUOIDUNG> TimKiem(List<NGUOIDUNG> user, string tenUser, DateTime? NgayDangKy)
        {
            var data = (from item in user
                        where (string.IsNullOrEmpty(tenUser) == true || item.HoTen.ToLower().Contains(tenUser.ToLower()) == true)
                        && (NgayDangKy == null || item.NgayDangKi == NgayDangKy)
                        select item).ToList();
            return data;
        }
        public ActionResult DanhSachNguoiDung(int? page, string tenUser, DateTime? NgayDangKy)
        {
            //if (Session["AdminTK"] == null || string.IsNullOrEmpty(Session["AdminTK"].ToString()))
            //{
            //    return RedirectToAction("Login", "Admin");
            //}
            int pageSize = 10;
            int pageNum = (page ?? 1);
            var DSUser = db.NGUOIDUNGs.ToList();
            //Tìm kiếm
            var ketQua = TimKiem(DSUser, tenUser, NgayDangKy);
            return View(ketQua.OrderBy(x => x.MaND).ToPagedList(pageNum, pageSize));
        }
        public ActionResult ChiTietUser(int id)
        {
            var user = db.NGUOIDUNGs.SingleOrDefault(item => item.MaND == id); ;
            return View(user);
        }
        //Thêm người dùng
        [HttpGet]
        public ActionResult ThemUser()
        {
            //if (Session["AdminTK"] == null || string.IsNullOrEmpty(Session["AdminTK"].ToString()))
            //{
            //    return RedirectToAction("Login", "Admin");
            //}
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemUser(NGUOIDUNG user)
        {
            // Kiểm tra ModelState hợp lệ
            if (!ModelState.IsValid)
            {
                ViewBag.Thongbao = "Thông tin nhập vào không hợp lệ!";
                return View(user);
            }
            var existingUser = db.NGUOIDUNGs.FirstOrDefault(u => u.TaiKhoan == user.TaiKhoan || u.Email == user.Email);
            if (existingUser != null)
            {
                if (existingUser.TaiKhoan == user.TaiKhoan)
                    ModelState.AddModelError("TaiKhoan", "Tài khoản đã tồn tại.");
                if (existingUser.Email == user.Email)
                    ModelState.AddModelError("Email", "Email đã được sử dụng.");
                return View(user);
            }
            try
            {
                // Thêm chiến dịch vào cơ sở dữ liệu
                user.NgayDangKi = DateTime.Now;
                db.NGUOIDUNGs.Add(user);
                db.SaveChanges();

                // Chuyển hướng về trang danh sách chiến dịch
                return RedirectToAction("DanhSachNguoiDung");
            }
            catch (Exception ex)
            {
                // Xử lý lỗi và hiển thị thông báo
                ViewBag.Thongbao = $"Lỗi xảy ra: {ex.Message}";
                return View(user);
            }
        }

        //Sửa
        public ActionResult SuaUser(int id)
        {
            NGUOIDUNG user = db.NGUOIDUNGs.SingleOrDefault(x => x.MaND == id);
            if (user == null)
            {
                Response.StatusCode = 404;
            }
            return View(user);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SuaUser(NGUOIDUNG user)
        {
            // Truy xuất bản ghi từ database
            var model = db.NGUOIDUNGs.SingleOrDefault(x => x.MaND == user.MaND);
            if (user == null)
            {
                // Nếu không tìm thấy chiến dịch
                return HttpNotFound();
            }
            var existingUser = db.NGUOIDUNGs.FirstOrDefault(u => u.TaiKhoan == user.TaiKhoan || u.Email == user.Email);
            if (existingUser != null && existingUser.MaND != user.MaND)
            {
                if (existingUser.TaiKhoan == user.TaiKhoan)
                    ModelState.AddModelError("TaiKhoan", "Tài khoản đã tồn tại.");
                if (existingUser.Email == user.Email)
                    ModelState.AddModelError("Email", "Email đã được sử dụng.");
                return View(user);
            }
            try
            {
                if (model != null)
                {
                    model.HoTen = user.HoTen;
                    model.TaiKhoan = user.TaiKhoan;
                    model.MatKhau = user.MatKhau;
                    model.Email = user.Email;
                    model.NgaySinh = user.NgaySinh;
                    model.NgayDangKi =  model.NgayDangKi;
                    db.SaveChanges();
                }

                // Chuyển hướng sau khi thành công
                return RedirectToAction("DanhSachNguoiDung");
            }
            catch (Exception ex)
            {
                ViewBag.Thongbao = $"Lỗi xảy ra: {ex.Message}";
            }

            return View(model);
        }

        //Xoá
        [HttpGet]
        public ActionResult XoaUser(int id)
        {
            NGUOIDUNG user = db.NGUOIDUNGs.SingleOrDefault(x => x.MaND == id);
            ViewBag.MaND = user.MaND;
            if (user == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(user);
        }
        [HttpPost, ActionName("XoaUser")]
        public ActionResult XacNhanXoa(int id)
        {
            NGUOIDUNG user = db.NGUOIDUNGs.SingleOrDefault(x => x.MaND == id);
            ViewBag.MaND = user.MaND;
            if (user == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.NGUOIDUNGs.Remove(user);
            db.SaveChanges();
            return RedirectToAction("DanhSachNguoiDung");
        }

        public ActionResult ThongKeUser(string tenUser, DateTime? NgayDangKy, int? year)
        {
            ViewBag.TongNguoiDung = db.NGUOIDUNGs.Select(x => x.MaND).Count();
            var newUser = db.NGUOIDUNGs
                           .GroupBy(x => new { thang = x.NgayDangKi.Value.Month, nam = x.NgayDangKi.Value.Year })
                           .Select(y => new { Thang = y.Key.thang, Nam = y.Key.nam, SoLuongUser = y.Count() })
                           .OrderBy(y => y.Nam).ThenBy(y => y.Thang)
                           .ToList();
            ViewBag.UserData = Newtonsoft.Json.JsonConvert.SerializeObject(newUser);
            var userList = db.NGUOIDUNGs.ToList();
            var ketQua = TimKiem(userList, tenUser, NgayDangKy);
            return View(ketQua.OrderByDescending(x => x.NgayDangKi));
        }
        public ActionResult getUserByYear(int year)
        {
            var yearList = db.NGUOIDUNGs.Select(x => x.NgayDangKi.Value.Year)
               .Distinct()
               .OrderBy(x => x)
               .ToList();
            return Json(yearList, JsonRequestBehavior.AllowGet);
        }
    }
}