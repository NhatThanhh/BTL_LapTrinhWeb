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
        public List<CHIENDICH> TimKiem(List<CHIENDICH> chiendich, string tenChienDich)
        {
            var data = (from item in chiendich where(string.IsNullOrEmpty(tenChienDich) == true || item.TenCD.ToLower().Contains(tenChienDich.ToLower()) == true)
                        select item).ToList();
            return data;
        }
        public ActionResult Index(int? page, string tenChienDich)
        {
            int pageSize = 8;
            int pageNum = (page ?? 1);
            var CDlist = listCD(100);
            if (!string.IsNullOrEmpty(tenChienDich))
            {
                CDlist = CDlist
                    .Where(cd => cd.TenCD.ToLower().Contains(tenChienDich.ToLower()))
                    .ToList();

                if (!CDlist.Any())
                {
                    ViewBag.ErrorMessage = $"Không tìm thấy chiến dịch nào với từ khóa '{tenChienDich}'.";
                    return View(new List<CHIENDICH>().ToPagedList(pageNum, pageSize));
                }
            }
            return View(CDlist.OrderBy(x => x.MaCD).ToPagedList(pageNum, pageSize));
        }
        [HttpGet]
        public ActionResult chiTietCD(int id)
        {
            var chienDich = db.CHIENDICHes.SingleOrDefault(item => item.MaCD == id);
            var quanLy = db.QUANLies.Where(s => s.MaQL == chienDich.MaQL).SingleOrDefault();
            ViewBag.NgayTao = chienDich.NgayTao?.ToString("dd/MM/yyyy");
            ViewBag.TenQL = quanLy?.TenQL ?? "Không xác định";
            var chiTietQuyenGopData = db.CHITIETQUYENGOPs.Where(ctqg => ctqg.MaCD == id)
                .Join(db.QUYENGOPs, ctqg => ctqg.MaQG, qg => qg.MaQG, (ctqg, qg) => new { ctqg, qg })
                .Join(db.NGUOIDUNGs, combined => combined.qg.MaND, nd => nd.MaND, (combined, nd) => new { combined, nd })
                .Select(x => new ChiTietQuyenGopVM
                {
                    HoTenNguoiDung = x.nd.HoTen,
                    TenChienDich = chienDich.TenCD,
                    SoTienQuyenGop = (decimal)x.combined.ctqg.SoTienQG,
                    NgayQuyenGop = x.combined.qg.NgayQuyenGop.Value
                })
                .ToList();
            ViewBag.ChiTietQuyenGop = chiTietQuyenGopData;
            return View(chienDich);
        }

    }
}