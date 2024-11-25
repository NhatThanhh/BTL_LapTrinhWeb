using BTL_CharityWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTL_CharityWebsite.ViewModel
{
    public class DonateVM
    {
        CharityManagementEntities db = new CharityManagementEntities();
        public int iMaCD {  get; set; }
        public string iTenCD {  get; set; }
        public string iAnhbia { get; set; }
        public double iTongquy { get; set; }
        public double iSoTien { get; set; }
        public int TypePayment {  get; set; }
        public int TypePaymentVN { get; set; }
        public DonateVM(int MaCD)
        {
            iMaCD = MaCD;
            CHIENDICH chiendich = db.CHIENDICHes.Single(n => n.MaCD == MaCD);
            iTenCD = chiendich.TenCD;
            iAnhbia = chiendich.AnhBia;
            iTongquy = double.Parse(chiendich.TongQuy.ToString());
        }
    }
}