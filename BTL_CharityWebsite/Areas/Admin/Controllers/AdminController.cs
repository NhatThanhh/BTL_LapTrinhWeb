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
    public class AdminController : Controller
    {
        private readonly CharityManagementEntities db;
        public AdminController(CharityManagementEntities db)
        {
            this.db = db;
        }
        public AdminController() : this(new CharityManagementEntities()) { }

        // GET: Admin/Home
        public ActionResult Index()
        {
            if (Session["AdminTK"] == null || string.IsNullOrEmpty(Session["AdminTK"].ToString()))
            {
                return RedirectToAction("Login", "Admin");
            }
            return View();
        }

        // Admin Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection c)
        {
            var tendn = c["username"];
            var matkhau = c["password"];
            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loitendn"] = "Tên đăng nhập không được để trống";
            }
            if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loimk"] = "Mật khẩu không được để trống";
            }
            if (String.IsNullOrEmpty(ViewData["Loitendn"]?.ToString()) && string.IsNullOrEmpty(ViewData["Loimk"]?.ToString()))
            {
                ADMIN admin = db.ADMINs.FirstOrDefault(x => x.UserAdmin == tendn && x.PassAdmin == matkhau);
                if (admin != null)
                {
                    Session["AdminTK"] = admin;
                    return RedirectToAction("Index","Dashboard");
                }
                else
                {
                    ViewData["Loi"] = "Tên đăng nhập hoặc mật khẩu không đúng";
                }
            }
            return View();
        }

        //Tìm kiếm
        public List<CHIENDICH> TimKiem(List<CHIENDICH> chiendich, int? namTao, string tenChienDich, decimal? minTongQuy, decimal? maxTongQuy)
        {
            var data = (from item in chiendich where (namTao == null || item.NgayTao.Value.Year == namTao)
                        && (string.IsNullOrEmpty(tenChienDich) == true|| item.TenCD.ToLower().Contains(tenChienDich.ToLower()) == true)
                        && (minTongQuy == null || item.TongQuy >= minTongQuy)
                        && (maxTongQuy == null || item.TongQuy <= maxTongQuy)
                        select item).ToList();
            return data;
        }
        public ActionResult ChienDich(int? page, int? namTao, string tenChienDich, decimal? minTongQuy, decimal? maxTongQuy)
        {
            if (Session["AdminTK"] == null || string.IsNullOrEmpty(Session["AdminTK"].ToString()))
            {
                return RedirectToAction("Login", "Admin");
            }
            int pageSize = 6;
            int pageNum = (page ?? 1);
            //Toàn bộ danh sách
            var DSChienDich = db.CHIENDICHes.ToList();
            //Tìm kiếm
            var ketQua = TimKiem(DSChienDich, namTao, tenChienDich, minTongQuy, maxTongQuy);
            return View(ketQua.OrderBy(x => x.MaCD).ToPagedList(pageNum, pageSize));
        }


        [HttpGet]
        public ActionResult ThemChienDich()
        {
            if (Session["AdminTK"] == null || string.IsNullOrEmpty(Session["AdminTK"].ToString()))
            {
                return RedirectToAction("Login", "Admin");
            }
            ViewBag.MaQL = new SelectList(db.QUANLies.ToList().OrderBy(x => x.TenQL), "MaQL", "TenQL");
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemChienDich(CHIENDICH chienDich, HttpPostedFileBase fileupload)
        {
            ViewBag.MaQL = new SelectList(db.QUANLies.ToList().OrderBy(x => x.TenQL), "MaQL", "TenQL");

            // Kiểm tra file upload
            if (fileupload == null)
            {
                ViewBag.Thongbao = "Vui lòng chọn ảnh bìa!";
                return View(chienDich);
            }

            // Kiểm tra tên chiến dịch đã tồn tại chưa
            if (db.CHIENDICHes.Any(x => x.TenCD == chienDich.TenCD))
            {
                ViewBag.Thongbao = "Tên chiến dịch đã tồn tại, vui lòng nhập tên khác!";
                return View(chienDich);
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Thongbao = "Thông tin nhập vào không hợp lệ!";
                return View(chienDich);
            }

            try
            {
                // Lấy tên file và đường dẫn lưu
                var fileName = Path.GetFileName(fileupload.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/images"), fileName);

                // Kiểm tra nếu file đã tồn tại
                if (System.IO.File.Exists(path))
                {
                    ViewBag.Thongbao = "Hình ảnh đã tồn tại, vui lòng chọn ảnh khác!";
                    return View(chienDich);
                }
                fileupload.SaveAs(path);
                chienDich.AnhBia = fileName;
                db.CHIENDICHes.Add(chienDich);
                db.SaveChanges();

                return RedirectToAction("ChienDich");
            }
            catch (Exception ex)
            {
                ViewBag.Thongbao = $"Lỗi xảy ra: {ex.Message}";
                return View(chienDich);
            }
        }
        //chi tiết chiến dịch
        public ActionResult ChiTietChienDich(int id)
        {
            var chienDich = db.CHIENDICHes.SingleOrDefault(item => item.MaCD == id);
            var quanLy = db.QUANLies.Where(s => s.MaQL == chienDich.MaQL).SingleOrDefault();
            ViewBag.NgayTao = chienDich.NgayTao?.ToString("dd/MM/yyyy");
            ViewBag.TenQL = quanLy?.TenQL ?? "Không xác định";
            return View(chienDich);
        }
        //Xoá chiến dịch


        [HttpGet]
        public ActionResult XoaChienDich(int id)
        {
            CHIENDICH ChienDich = db.CHIENDICHes.SingleOrDefault(x => x.MaCD == id);
            ViewBag.MaCD = ChienDich.MaCD;
            if (ChienDich == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(ChienDich);
        }
        [HttpPost, ActionName("XoaChienDich")]
        public ActionResult XacNhanXoa(int id)
        {
            CHIENDICH ChienDich = db.CHIENDICHes.SingleOrDefault(x => x.MaCD == id);
            ViewBag.MaCD = ChienDich.MaCD;
            if (ChienDich == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.CHIENDICHes.Remove(ChienDich);
            db.SaveChanges();
            return RedirectToAction("ChienDich");
        }

        // Sửa thông tin
        [HttpGet]
        public ActionResult SuaChienDich(int id)
        {
            CHIENDICH ChienDich = db.CHIENDICHes.SingleOrDefault(x => x.MaCD == id);
            if (ChienDich == null)
            {
                Response.StatusCode = 404;
            }
            ViewBag.MaQL = new SelectList(db.QUANLies.ToList().OrderBy(x => x.MaQL == ChienDich.MaQL ? 0 : 1), "MaQL", "TenQL", ChienDich.MaQL);
            return View(ChienDich);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SuaChienDich(CHIENDICH ChienDich, HttpPostedFileBase fileupload)
        {
            var model = db.CHIENDICHes.SingleOrDefault(x => x.MaCD == ChienDich.MaCD);
            ViewBag.MaQL = new SelectList(db.QUANLies.ToList().OrderBy(x => x.MaQL == ChienDich.MaQL ? 0 : 1), "MaQL", "TenQL", ChienDich.MaQL);
            if (ChienDich == null)
            {
                return HttpNotFound();
            }

            // Xử lý ảnh
            if (fileupload != null && fileupload.ContentLength > 0)
            {
                var fileName = Path.GetFileName(fileupload.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/images"), fileName);

                if (!System.IO.File.Exists(path)) 
                {
                    fileupload.SaveAs(path);
                    ChienDich.AnhBia = fileName;
                }
                else
                {
                    ViewBag.Thongbao = "Hình ảnh đã tồn tại, vui lòng chọn ảnh khác!";
                    return View(ChienDich);
                }
            }
            else
            {
                ChienDich.AnhBia = model.AnhBia;
            }

            try
            {
                if(model != null)
                {
                    model.TenCD = ChienDich.TenCD;
                    model.MaQL = ChienDich.MaQL;
                    model.MoTa = ChienDich.MoTa;
                    model.AnhBia = ChienDich.AnhBia;
                    model.TongQuy = ChienDich.TongQuy;
                    model.NgayTao = ChienDich.NgayTao;
                    db.SaveChanges();
                }

                // Chuyển hướng sau khi thành công
                return RedirectToAction("ChienDich");
            }
            catch (Exception ex)
            {
                ViewBag.Thongbao = $"Lỗi xảy ra: {ex.Message}";
            }
            
            return View(model);
        }
        public ActionResult DangXuat()
        {
            Session["AdminTK"] = null;
            return RedirectToAction("Login");
        }

    }
}