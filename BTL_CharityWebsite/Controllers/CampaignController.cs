using BTL_CharityWebsite.Models;
using BTL_CharityWebsite.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace BTL_CharityWebsite.Controllers
{
    public class CampaignController : Controller
    {
        private readonly CharityManagementEntities db;
        public CampaignController(CharityManagementEntities db)
        {
            this.db = db;
        }
        public CampaignController() : this(new CharityManagementEntities()) { }
        // GET: Team
        private List<CHIENDICH> listCD(int count)
        {
            return db.CHIENDICHes.Take(count).ToList();
        }
        // GET: Campaign
        public ActionResult Index(int? page)
        {
            int pageSize = 8;
            int pageNum = (page ?? 1);
            var CDlist = listCD(16);
            return View(CDlist.ToPagedList(pageNum,pageSize));
        }
        [HttpGet]
        public ActionResult chiTietCD(int id)
        {
            var chienDich = db.CHIENDICHes.SingleOrDefault(item => item.MaCD == id);
            var quanLy = db.QUANLies.Where(s => s.MaQL == chienDich.MaQL).SingleOrDefault();
            ViewBag.NgayTao = chienDich.NgayTao?.ToString("dd/MM/yyyy");
            ViewBag.TenQL = quanLy?.TenQL ?? "Không xác định";
            return View(chienDich);
        }

    }
}