using BTL_CharityWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using System.Web.UI;

namespace BTL_CharityWebsite.Controllers
{
    public class HomeController : Controller
    {
        private readonly CharityManagementEntities db;
        public HomeController(CharityManagementEntities db)
        {
            this.db = db;
        }
        public HomeController() : this(new CharityManagementEntities()) { }

        // Thư viện
        public ActionResult ThuVien(int? MaCD, int? page = 1)
        {
            int pageSize = 9; // Số bản ghi mỗi trang
            int currentPage = page ?? 1; // Trang hiện tại

            // Lấy danh sách tất cả các chiến dịch
            var listCD = db.CHIENDICHes.ToList();

            // Lấy danh sách hình ảnh từ bảng THUVIEN dựa trên MaCD
            var listTV = db.THUVIENs
                .Where(x => MaCD == null || x.MaCD == MaCD)  // Chỉ lấy hình ảnh theo chiến dịch
                .OrderBy(x => x.MaHA)
                .ToPagedList(currentPage, pageSize);  // Tạo IPagedList

            // Truyền danh sách chiến dịch và hình ảnh vào view
            return View(Tuple.Create(listCD, listTV, MaCD));
        }


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}