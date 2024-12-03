using BTL_CharityWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using System.IO;
using System.Web.UI.WebControls;
using System.Data.Entity;

namespace BTL_CharityWebsite.Areas.Admin.Controllers
{
    public class LibraryController : Controller
    {
        private readonly CharityManagementEntities db;
        public LibraryController(CharityManagementEntities db)
        {
            this.db = db;
        }
        public LibraryController() : this(new CharityManagementEntities()) { }
        // GET: Admin/Library
        public ActionResult Index()
        {
            return View();
        }

        //Danh sách chiến dịch
        public ActionResult DanhSachChienDich(int? page = 1)
        {
            if (Session["AdminTK"] == null || string.IsNullOrEmpty(Session["AdminTK"].ToString()))
            {
                return RedirectToAction("Login", "Admin");
            }
            int pageSize = 10;
            int pageNum = (page ?? 1);

            // Lấy danh sách chiến dịch phân trang
            var listChienDich = db.CHIENDICHes
                .OrderBy(x => x.MaCD)
                .ToPagedList(pageNum, pageSize);

            return View(listChienDich);
        }


        //Thư viện
        public ActionResult ThuVien(int? MaCD, int? page = 1)
        {
            int pageSize = 9;
            int pageNum = (page ?? 1);
            var imagelist = db.THUVIENs.Where(x => x.MaCD == MaCD).OrderBy(x => x.MaHA).ToPagedList(pageNum, pageSize);
            ViewBag.MaCD = MaCD;
            ViewBag.TenCD = db.CHIENDICHes.Where(x => x.MaCD == MaCD).Select(x => x.TenCD).SingleOrDefault();
            return View(imagelist);
        }
        //xoá ảnh
        [HttpPost]
        public ActionResult XoaAnh(int[] AnhChon, int? MaCD)
        {
            if(AnhChon == null || AnhChon.Length == 0)
            {
                TempData["thongbao"] = "Vui lòng chọn ảnh để xoá.";
                return RedirectToAction("ThuVien", new { MaCD = MaCD });
            }
            foreach (var MaHA  in AnhChon)
            {
                var anh = db.THUVIENs.SingleOrDefault( x=> x.MaHA == MaHA );

                if(anh != null)
                {
                    string path = Server.MapPath("~/Content/images/" + anh.Anh);
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                    db.THUVIENs.Remove(anh);
                }
            }
            db.SaveChanges();
            return RedirectToAction("ThuVien", new { MaCD = MaCD });
        }

        //Thêm ảnh
        public ActionResult ThemAnh(THUVIEN thuvien, int MaCD, HttpPostedFileBase[] fileupload)
        {
            if (fileupload.All(file => file == null || file.ContentLength == 0))
            {
                TempData["thongbaoThem"] = "Vui lòng chọn ít nhất một file để tải lên.";
                return RedirectToAction("ThuVien", new { MaCD = MaCD });
            }

            foreach (var file in fileupload)
            {
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/images"), fileName);
                    if (System.IO.File.Exists(path))
                    {
                        continue;
                    }
                    try
                    {
                        file.SaveAs(path);
                        thuvien.Anh = fileName;
                        thuvien.MaCD = MaCD;
                        thuvien.NgayDang = DateTime.Now;
                        db.THUVIENs.Add(thuvien);
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        TempData["thongbaoThem"] = $"Lỗi khi thêm file {fileName}: {ex.Message}.";
                    }
                }

            }
            TempData["thongbaoThem"] = "Thêm ảnh thành công!";
            return RedirectToAction("ThuVien", new { MaCD = MaCD });
        }
    }
}
