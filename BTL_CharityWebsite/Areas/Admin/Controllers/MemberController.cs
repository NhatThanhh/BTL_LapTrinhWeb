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
using System.Web.UI;

namespace BTL_CharityWebsite.Areas.Admin.Controllers
{
    public class MemberController : Controller
    {
        private readonly CharityManagementEntities db;
        public MemberController(CharityManagementEntities db)
        {
            this.db = db;
        }
        public MemberController() : this(new CharityManagementEntities()) { }
        // GET: Admin/Member
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DanhSachThanhVien(int? page)
        {
            //if (Session["AdminTK"] == null || string.IsNullOrEmpty(Session["AdminTK"].ToString()))
            //{
            //    return RedirectToAction("Login", "Admin");
            //}
            int pageSize = 6;
            int pageNum = (page ?? 1);
            return View(db.QUANLies.ToList().OrderBy(x => x.MaQL).ToPagedList(pageNum, pageSize));
        }

        public ActionResult ChiTietThanhVien(int id)
        {
            var ThanhVien = db.QUANLies.SingleOrDefault(item => item.MaQL == id);;
            return View(ThanhVien);
        }


        //Thêm
        [HttpGet]
        public ActionResult ThemThanhVien()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemThanhVien(QUANLY thanhvien, HttpPostedFileBase fileupload)
        {
            // Kiểm tra file upload
            if (fileupload == null)
            {
                ViewBag.Thongbao = "Vui lòng chọn ảnh bìa!";
                return View(thanhvien);
            }

            // Kiểm tra ModelState hợp lệ
            if (!ModelState.IsValid)
            {
                ViewBag.Thongbao = "Thông tin nhập vào không hợp lệ!";
                return View(thanhvien);
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
                    return View(thanhvien);
                }

                // Lưu file lên server
                fileupload.SaveAs(path);

                // Gán đường dẫn file cho model
                thanhvien.AnhQL = fileName;

                // Thêm chiến dịch vào cơ sở dữ liệu
                db.QUANLies.Add(thanhvien);
                db.SaveChanges();

                // Chuyển hướng về trang danh sách chiến dịch
                return RedirectToAction("DanhSachThanhVien");
            }
            catch (Exception ex)
            {
                // Xử lý lỗi và hiển thị thông báo
                ViewBag.Thongbao = $"Lỗi xảy ra: {ex.Message}";
                return View(thanhvien);
            }
        }

        //Xoá
        [HttpGet]
        public ActionResult XoaThanhVien(int id)
        {
            QUANLY thanhvien = db.QUANLies.SingleOrDefault(x => x.MaQL == id);
            ViewBag.MaQL = thanhvien.MaQL;
            if (thanhvien == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(thanhvien);
        }
        [HttpPost, ActionName("XoaThanhVien")]
        public ActionResult XacNhanXoa(int id)
        {
            QUANLY thanhvien = db.QUANLies.SingleOrDefault(x => x.MaQL == id);
            if (thanhvien == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MaQL = thanhvien.MaQL;
            try
            {
                // Xóa file ảnh nếu tồn tại
                string path = Server.MapPath("~/Content/images/" + thanhvien.AnhQL);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }

                // Xóa bản ghi khỏi database
                db.QUANLies.Remove(thanhvien);
                db.SaveChanges();

                TempData["Message"] = "Xóa thành viên thành công!";
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Có lỗi xảy ra khi xóa: {ex.Message}";
                return RedirectToAction("DanhSachThanhVien");
            }
            return RedirectToAction("DanhSachThanhVien");
        }

        //Chỉnh sửa
        [HttpGet]
        public ActionResult SuaThanhVien(int id)
        {
            QUANLY thanhvien = db.QUANLies.SingleOrDefault(x => x.MaQL == id);
            if (thanhvien == null)
            {
                Response.StatusCode = 404;
            }
            return View(thanhvien);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SuaThanhVien(QUANLY thanhvien, HttpPostedFileBase fileupload)
        {
            var model = db.QUANLies.SingleOrDefault(x => x.MaQL == thanhvien.MaQL);
            if(thanhvien == null)
            {
                return HttpNotFound();
            }
            if (fileupload != null && fileupload.ContentLength > 0)
            {
                var fileName = Path.GetFileName(fileupload.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/images"), fileName);

                if (!System.IO.File.Exists(path)) // Nếu file chưa tồn tại
                {
                    fileupload.SaveAs(path);
                    thanhvien.AnhQL = fileName;
                }
                else
                {
                    ViewBag.Thongbao = "Hình ảnh đã tồn tại, vui lòng chọn ảnh khác!";
                    return View(thanhvien);
                }
            }
            else
            {
                // Nếu không chọn ảnh mới, giữ lại ảnh hiện tại
                thanhvien.AnhQL = model.AnhQL;
            }
            try
            {
                if (model != null)
                {
                    model.TenQL = thanhvien.TenQL;
                    model.EmailQL = thanhvien.EmailQL;
                    model.SDTQL = thanhvien.SDTQL;
                    model.AnhQL = thanhvien.AnhQL;
                    model.TieuSu = thanhvien.TieuSu;
                    model.ChucVu = thanhvien.ChucVu;
                    db.SaveChanges();
                }

                // Chuyển hướng sau khi thành công
                ViewBag.Thongbao = "Cập nhật thông tin thành viên thành công";
                return RedirectToAction("DanhSachThanhVien");
            }
            catch (Exception ex)
            {
                ViewBag.Thongbao = $"Lỗi xảy ra: {ex.Message}";
            }

            return View(model);
        }
        
    }
}