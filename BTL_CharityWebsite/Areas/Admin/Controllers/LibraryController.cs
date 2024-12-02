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
            int pageSize = 6;
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
            return View(imagelist);
        }
    }
}
