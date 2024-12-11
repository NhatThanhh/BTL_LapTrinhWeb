//using BTL_CharityWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BTL_CharityWebsite.Models;
using BTL_CharityWebsite.ViewModel;
using System.Threading.Tasks;
using System.Data.Entity;

namespace BTL_CharityWebsite.Controllers
{
    public class UserController : Controller
    {
        private readonly CharityManagementEntities db;
        public UserController(CharityManagementEntities db)
        {
            this.db = db;
        }
        public UserController() : this(new CharityManagementEntities()) { }
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DangKy()
        {
            return View(new RegisterVM());
        }
        [HttpPost]
        public ActionResult DangKy(RegisterVM nd)
        {
            if (!ModelState.IsValid)
            {
                return View(nd);
            }

            var existingUser = db.NGUOIDUNGs.FirstOrDefault(u => u.TaiKhoan == nd.TaiKhoan);
            if (existingUser != null)
            {
                ModelState.AddModelError("TaiKhoan", "Tài khoản đã tồn tại.");
                return View(nd);
            }

            var existingEmail = db.NGUOIDUNGs.FirstOrDefault(u => u.Email == nd.Email);
            if (existingEmail != null)
            {
                ModelState.AddModelError("Email", "Email đã được sử dụng.");
                return View(nd);
            }

            var user = new NGUOIDUNG()
            {
                HoTen = nd.HoTen,
                TaiKhoan = nd.TaiKhoan,
                MatKhau = nd.MatKhau,
                Email = nd.Email,
                DiaChi = nd.DiaChi,
                SDT = nd.SDT,
                NgaySinh = nd.NgaySinh,
                NgayDangKi = DateTime.Now
            };

            try
            {
                db.NGUOIDUNGs.Add(user);
                db.SaveChanges();

                return RedirectToAction("DangNhap");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Đã có lỗi xảy ra, vui lòng thử lại.");
                return View(nd);
            }
        }
        public ActionResult DangNhap()
        {
            return View(new RegisterVM());
        }
        [HttpPost]
        public ActionResult Dangnhap(FormCollection collection)
        {
            var tendn = collection["TenDN"];
            var matkhau = collection["MatKhau"];
            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "Tên đăng nhập không được để trống";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Mật khẩu không được để trống";
            }
            // Nếu có lỗi, trả lại View
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Kiểm tra tài khoản và mật khẩu
            var ngd = db.NGUOIDUNGs.SingleOrDefault(n => n.TaiKhoan == tendn && n.MatKhau == matkhau);
            if (ngd != null)
            {
                // Đăng nhập thành công
                Session["TaiKhoan"] = ngd;
                if (TempData["ReturnUrl"] != null)
                {
                    return Redirect(TempData["ReturnUrl"].ToString());
                }
                return RedirectToAction("Index", "Home");
            }

            // Sai tài khoản hoặc mật khẩu
            ViewData["Loi3"] = "Tên đăng nhập hoặc mật khẩu không đúng !";
            return View();
        }



        //Lấy thông tin người dùng
        public ActionResult ThongTinNguoiDung()
        {
            var user = (NGUOIDUNG)Session["TaiKhoan"];
            if (user == null)
            {
                return RedirectToAction("DangNhap");
            }
            return View(user);
        }


        //Chỉnh sửa thông tin
        [HttpGet]
        public ActionResult ChinhSuaThongTin()
        {
            if (Session["TaiKhoan"] == null || string.IsNullOrEmpty(Session["TaiKhoan"].ToString()))
            {
                return RedirectToAction("DangNhap", "User");
            }

            NGUOIDUNG currentUser = (NGUOIDUNG)Session["TaiKhoan"];
            return View(currentUser); // Truyền thông tin người dùng hiện tại vào View
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChinhSuaThongTin(NGUOIDUNG user)
        {
            if (Session["TaiKhoan"] == null || string.IsNullOrEmpty(Session["TaiKhoan"].ToString()))
            {
                return RedirectToAction("DangNhap", "User");
            }

            // Lấy thông tin người dùng hiện tại từ Session
            NGUOIDUNG currentUser = (NGUOIDUNG)Session["TaiKhoan"];

            // Tìm người dùng từ cơ sở dữ liệu theo ID
            var userInDb = db.NGUOIDUNGs.SingleOrDefault(u => u.MaND == currentUser.MaND);
            if (userInDb != null)
            {
                var existingUser = db.NGUOIDUNGs.FirstOrDefault(u => u.Email == user.Email);
                if (existingUser != null && existingUser.MaND != userInDb.MaND)
                {
                    if (existingUser.Email == user.Email)
                        ModelState.AddModelError("Email", "Email đã được sử dụng.");
                    return View(userInDb);
                }
                // Cập nhật thông tin từ ViewModel (người dùng chỉ được chỉnh sửa các trường cần thiết)
                userInDb.HoTen = user.HoTen;
                userInDb.Email = user.Email;
                userInDb.SDT = user.SDT;
                userInDb.DiaChi = user.DiaChi;
                
                // Lưu thay đổi vào cơ sở dữ liệu
                db.SaveChanges();

                // Cập nhật Session với thông tin mới
                Session["TaiKhoan"] = userInDb;
                
                TempData["TBThanhCong"] = "Cập nhật thông tin thành công!";
                return RedirectToAction("ChinhSuaThongTin");
            }
            else
            {
                TempData["TBLoi"] = "Không tìm thấy người dùng!";
                return RedirectToAction("DangNhap", "User");
            }
        }
        [HttpGet]
        public ActionResult DoiMatKhau()
        {
            if (Session["TaiKhoan"] == null || string.IsNullOrEmpty(Session["TaiKhoan"].ToString()))
            {
                return RedirectToAction("DangNhap", "User");
            }

            return View();
        }

        //Đổi mật khẩu
        [HttpPost]
        public ActionResult DoiMatKhau(FormCollection c)
        {
            if (Session["TaiKhoan"] == null || string.IsNullOrEmpty(Session["TaiKhoan"].ToString()))
            {
                return RedirectToAction("DangNhap", "User");
            }
            NGUOIDUNG currentUser = (NGUOIDUNG)Session["TaiKhoan"];
            var userDb = db.NGUOIDUNGs.SingleOrDefault(u => u.MaND == currentUser.MaND);
            
            if(userDb == null)
            {
               
                return RedirectToAction("DangNhap", "User");
            }
            string _MatKhauCu = Request.Form["MatKhauCu"];
            string _MatKhauMoi = Request.Form["MatKhauMoi"];

            if(userDb.MatKhau != _MatKhauCu)
            {
                ViewBag.Loi1 = "Mật khẩu hiện tại không chính xác";
                return View();
            }

            if (string.IsNullOrEmpty(_MatKhauMoi))
            {
                ViewBag.Loi2 = "Vui lòng nhập mật khẩu mới";
                return View();
            }

            if (_MatKhauCu == _MatKhauMoi)
            {
                ViewBag.Loi2 = "Mật khẩu mới không được trùng với mật khẩu hiện tại";
                return View();
            }

            if (userDb != null)
            {
                userDb.MatKhau = _MatKhauMoi;

                // Lưu thay đổi vào cơ sở dữ liệu
                db.SaveChanges();

                // Cập nhật Session với thông tin mới
                Session["TaiKhoan"] = userDb;

                TempData["SuccessMessage"] = "Đổi mật khẩu thành công!";
                return RedirectToAction("ChinhSuaThongTin");
            }

            return RedirectToAction("ChinhSuaThongTin");
        }
        [HttpGet]
        public ActionResult QuenMatKhau() { return View(); }
        //Quên mật khẩu
        [HttpPost]
        public ActionResult QuenMatKhau(string key)
        {
            var user = db.NGUOIDUNGs.FirstOrDefault(u => u.TaiKhoan == key || u.Email == key);
            if(user != null)
            {
                ViewBag.ThongBao = $"Mật khẩu tài khoản {user.TaiKhoan}  là: {user.MatKhau}";
            }
            else
            {
                ViewBag.ThongBao = "Tài khoản không tồn tại";
            }
            return View();
        }

        //Đăng xuất
        public ActionResult DangXuat()
        {
            Session["TaiKhoan"] = null;
            return RedirectToAction("DangNhap");
        }
        public ActionResult LichSuQuyenGop()
        {
            NGUOIDUNG user = (NGUOIDUNG)Session["TaiKhoan"];
            int id = user.MaND;
            var chiTietQuyenGopData = db.CHITIETQUYENGOPs
                .Join(db.QUYENGOPs, ctqg => ctqg.MaQG, qg => qg.MaQG, (ctqg, qg) => new { ctqg, qg })
                .Where(x => x.qg.MaND == id)
                .Join(db.CHIENDICHes, combined => combined.ctqg.MaCD, cd => cd.MaCD, (combined, cd) => new { combined, cd })
                .Select(x => new ChiTietQuyenGopVM
                {
                    MaQG = x.combined.qg.MaQG,
                    HoTenNguoiDung = db.NGUOIDUNGs.Where(nd => nd.MaND == id).Select(nd => nd.HoTen).FirstOrDefault(),
                    TenChienDich = x.cd.TenCD,
                    SoTienQuyenGop = (decimal)x.combined.ctqg.SoTienQG,
                    NgayQuyenGop = x.combined.qg.NgayQuyenGop.Value
                }).OrderByDescending(x => x.NgayQuyenGop).ToList();
            ViewBag.ChiTietQuyenGop = chiTietQuyenGopData;
            return View();
        }


    }
}