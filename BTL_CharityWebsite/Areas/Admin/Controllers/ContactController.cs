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
using OfficeOpenXml.Style;
using OfficeOpenXml;

namespace BTL_CharityWebsite.Areas.Admin.Controllers
{
    public class ContactController : Controller
    {
        private readonly CharityManagementEntities db;
        public ContactController(CharityManagementEntities db)
        {
            this.db = db;
        }
        public ContactController() : this(new CharityManagementEntities()) { }
        // GET: Admin/Contact
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DanhSachLienHe()
        {
            if (Session["AdminTK"] == null || string.IsNullOrEmpty(Session["AdminTK"].ToString()))
            {
                return RedirectToAction("Login", "Admin");
            }
            //Toàn bộ danh sách
            var DSLienHe = db.LIENHEs.ToList();
            return View(DSLienHe);
        }
        [HttpPost]
        public ActionResult ExportToExcel(List<List<string>> data)
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Contacts");
                int row = 1;
                worksheet.Cells[row, 1].Value = "STT";
                worksheet.Cells[row, 2].Value = "Họ và Tên";
                worksheet.Cells[row, 3].Value = "Số điện thoại";
                worksheet.Cells[row, 4].Value = "Email";
                worksheet.Cells[row, 5].Value = "Ngày";
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
                var excelData = package.GetAsByteArray();
                var contacts = db.LIENHEs.ToList();
                db.LIENHEs.RemoveRange(contacts);
                db.SaveChanges();
                return File(excelData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Contacts.xlsx");
            }
        }
    }
}