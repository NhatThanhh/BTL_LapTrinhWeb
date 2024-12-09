﻿using BTL_CharityWebsite.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using BTL_CharityWebsite.ViewModel;

namespace BTL_CharityWebsite.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        private readonly CharityManagementEntities db;
        public DashboardController(CharityManagementEntities db)
        {
            this.db = db;
        }
        public DashboardController() : this(new CharityManagementEntities()) { }
        // GET: Admin/Dashboard
        public ActionResult Index()
        {
            //Tính tổng số người dùng, dự án, số lần quyên góp thành công
            ViewBag.TongNguoiDung = db.NGUOIDUNGs.Select(x => x.MaND).Count();
            ViewBag.TongChienDich = db.CHIENDICHes.Select(x => x.MaCD).Count();
            ViewBag.TongQuyenGop = db.QUYENGOPs.Select(x => x.MaQG).Count();
            //Dữ liệu cho biểu đồ thống kê tổng quỹ từng dự án
            var data = db.CHIENDICHes.Select(x => new { x.TenCD, x.TongQuy}).ToList(); 
            ViewBag.ChartData = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            //Dữ liệu biểu đồ thống kê số lượt quyên góp theo tháng
            var QuyenGop = db.QUYENGOPs.GroupBy(x => new {thang = x.NgayQuyenGop.Value.Month, nam = x.NgayQuyenGop.Value.Year})
                                                .Select(y => new {Thang = y.Key.thang, Nam = y.Key.nam, SoLuotQuyenGop = y.Count()})
                                                .OrderBy(y => y.Nam).ThenBy(y => y.Thang)
                                                .ToList();
            ViewBag.QuyenGopTheoThang = Newtonsoft.Json.JsonConvert.SerializeObject(QuyenGop);
            // Lấy dữ liệu cho bảng chi tiết quyên góp
            var chiTietQuyenGopData = db.CHITIETQUYENGOPs
                .Join(db.QUYENGOPs, ctqg => ctqg.MaQG, qg => qg.MaQG, (ctqg, qg) => new { ctqg, qg })
                .Join(db.NGUOIDUNGs, combined => combined.qg.MaND, nd => nd.MaND, (combined, nd) => new { combined, nd })
                .Join(db.CHIENDICHes, combined => combined.combined.ctqg.MaCD, cd => cd.MaCD, (combined, cd) => new ChiTietQuyenGopVM
                {
                    HoTenNguoiDung = combined.nd.HoTen,
                    TenChienDich = cd.TenCD,
                    SoTienQuyenGop = (decimal)combined.combined.ctqg.SoTienQG,
                    NgayQuyenGop = combined.combined.qg.NgayQuyenGop.Value
                })
                .OrderByDescending(x => x.NgayQuyenGop)
                .ToList();

            ViewBag.ChiTietQuyenGopData = chiTietQuyenGopData.ToList();
            return View();
        }

        //tìm kiếm
        public List<ChiTietQuyenGopVM> TimKiem(List<ChiTietQuyenGopVM> qg, DateTime? ngay, string tuKhoa)
        {
            var data = (from item in qg
                        where (ngay == null || item.NgayQuyenGop.Date == ngay.Value.Date)
                        && (string.IsNullOrEmpty(tuKhoa) == true || item.TenChienDich.ToLower().Contains(tuKhoa.ToLower()) == true || item.HoTenNguoiDung.ToLower().Contains(tuKhoa.ToLower()) == true)
                        select item).ToList();
            return data;
        }

        public ActionResult ChiTietQuyenGop(DateTime? ngay, string tuKhoa, string sortBy)
        {
            var data = db.CHITIETQUYENGOPs.Join(db.QUYENGOPs,ctqg => ctqg.MaQG,qg => qg.MaQG,(ctqg, qg) => new { ctqg, qg })
                                        .Join(db.NGUOIDUNGs,combined => combined.qg.MaND,nd => nd.MaND,(combined, nd) => new { combined, nd })
                                        .Join(db.CHIENDICHes,combined => combined.combined.ctqg.MaCD,cd => cd.MaCD,(combined, cd) => new ChiTietQuyenGopVM
                                            {
                                                HoTenNguoiDung = combined.nd.HoTen,
                                                TenChienDich = cd.TenCD,
                                                SoTienQuyenGop = (decimal)combined.combined.ctqg.SoTienQG,
                                                NgayQuyenGop = combined.combined.qg.NgayQuyenGop.Value
                                            }).ToList();


            var ketQua = TimKiem(data, ngay, tuKhoa);
            if (sortBy == "asc")
            {
                ketQua = ketQua.OrderBy(x => x.SoTienQuyenGop).ToList();
            }
            else if (sortBy == "desc")
            {
                ketQua = ketQua.OrderByDescending(x => x.SoTienQuyenGop).ToList();
            }else if(sortBy == "ascDate")
            {
                ketQua = ketQua.OrderBy(x => x.NgayQuyenGop).ToList();
            }
            else if (sortBy == "descDate")
            {
                ketQua = ketQua.OrderByDescending(x => x.NgayQuyenGop).ToList();
            }
            return PartialView("ChiTietQuyenGop", ketQua);
        }

        public ActionResult ThongKeQuyenGop(DateTime? ngay, string tuKhoa)
        {
            ViewBag.TongQuyenGop = db.QUYENGOPs.Select(x => x.MaQG).Count();
            var yearList = db.QUYENGOPs.Select(x => x.NgayQuyenGop.Value.Year)
                   .Distinct()
                   .OrderByDescending(x => x)
                   .ToList();
            ViewBag.YearList = yearList;
            var chiTietQuyenGopData = db.CHITIETQUYENGOPs
                .Join(db.QUYENGOPs, ctqg => ctqg.MaQG, qg => qg.MaQG, (ctqg, qg) => new { ctqg, qg })
                .Join(db.NGUOIDUNGs, combined => combined.qg.MaND, nd => nd.MaND, (combined, nd) => new { combined, nd })
                .Join(db.CHIENDICHes, combined => combined.combined.ctqg.MaCD, cd => cd.MaCD, (combined, cd) => new ChiTietQuyenGopVM
                {
                    HoTenNguoiDung = combined.nd.HoTen,
                    TenChienDich = cd.TenCD,
                    SoTienQuyenGop = (decimal)combined.combined.ctqg.SoTienQG,
                    NgayQuyenGop = combined.combined.qg.NgayQuyenGop.Value
                })
                .ToList();
            return View(chiTietQuyenGopData.OrderByDescending(x => x.NgayQuyenGop));
        }

        public ActionResult GetDonationByYear(int year)
        {
            var quyenGop = db.QUYENGOPs.Where(x =>x.NgayQuyenGop.Value.Year == year)
                .GroupBy(x => new { thang = x.NgayQuyenGop.Value.Month, nam = x.NgayQuyenGop.Value.Year })
                                    .Select(y => new { Thang = y.Key.thang, Nam = y.Key.nam, SoLuotQuyenGop = y.Count() })
                                    .OrderBy(y => y.Nam).ThenBy(y => y.Thang)
                                    .ToList();
            return Json(quyenGop, JsonRequestBehavior.AllowGet);
        }

    }
}