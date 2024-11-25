using BTL_CharityWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTL_CharityWebsite.Controllers
{
    public class TeamController : Controller
    {
        private readonly CharityManagementEntities db;
        public TeamController(CharityManagementEntities db)
        {
            this.db = db;
        }
        public TeamController() : this(new CharityManagementEntities()) { }
        // GET: Team
        private List<QUANLY> listQL (int count)
        {
            return db.QUANLies.Take(count).ToList();
        }
        public ActionResult Index()
        {
            var quanlylist = listQL(4);
            return View(quanlylist);
        }
        [HttpGet]
        public ActionResult chiTietQL(int id)
        {
            var quanli = from item in db.QUANLies where item.MaQL == id select item;
            return View(quanli.Single());
        }
    }
}