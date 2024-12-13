using BTL_CharityWebsite.Models;
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
using OfficeOpenXml.Style;
using OfficeOpenXml;

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
            if (Session["AdminTK"] == null || string.IsNullOrEmpty(Session["AdminTK"].ToString()))
            {
                return RedirectToAction("Login", "Admin");
            }
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
            if (Session["AdminTK"] == null || string.IsNullOrEmpty(Session["AdminTK"].ToString()))
            {
                return RedirectToAction("Login", "Admin");
            }
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
            if (Session["AdminTK"] == null || string.IsNullOrEmpty(Session["AdminTK"].ToString()))
            {
                return RedirectToAction("Login", "Admin");
            }
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


        //Export Excel
        [HttpPost]
        public ActionResult ExportToExcel(List<List<string>> data)
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Donations");
                int row = 1;

                // Header
                worksheet.Cells[row, 1].Value = "STT";
                worksheet.Cells[row, 2].Value = "Họ và Tên";
                worksheet.Cells[row, 3].Value = "Tên dự án";
                worksheet.Cells[row, 4].Value = "Số tiền";
                worksheet.Cells[row, 5].Value = "Ngày";

                // Style Header
                for (int col = 1; col <= 5; col++)
                {
                    worksheet.Cells[row, col].Style.Font.Bold = true;
                    worksheet.Cells[row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[row, col].AutoFitColumns();
                }

                foreach (var rowData in data)
                {
                    row++;
                    for (int col = 0; col < rowData.Count; col++)
                    {
                        worksheet.Cells[row, col + 1].Value = rowData[col];
                    }
                }
                // Tạo file Excel
                var excelData = package.GetAsByteArray();
                return File(excelData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Donations.xlsx");
            }
        }

    }
}