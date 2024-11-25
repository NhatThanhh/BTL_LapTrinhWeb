
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Data.Entity;
using BTL_CharityWebsite.Models;
using BTL_CharityWebsite.ViewModel;
using BTL_CharityWebsite.Models.Payments;
using System.Configuration;


namespace BTL_CharityWebsite.Controllers
{
    public class DonateController : Controller
    {
        CharityManagementEntities db = new CharityManagementEntities();
        // GET: Donate
        public List<DonateVM> layQuyenGop()
        {
            var listQuyenGop = Session["quyenGop"] as List<DonateVM>;
            if (listQuyenGop == null)
            {
                //Nếu quyengop chưa tồn tại thì khởi tạo quyengop
                listQuyenGop = new List<DonateVM>();
                Session["quyenGop"] = listQuyenGop;
            }
            return listQuyenGop;
        }

        //Thêm vào donate 
        public ActionResult ThemQuyenGop(int iMaCD, string strURL, double tienQuyenGop)
        {
            //Lấy session quyên góp
            List<DonateVM> lstQuyenGop = layQuyenGop();
            DonateVM chiendich = lstQuyenGop.Find(x => x.iMaCD == iMaCD);
   
            if(chiendich == null)
            {
                chiendich = new DonateVM(iMaCD);
                chiendich.iTongquy += tienQuyenGop;
                lstQuyenGop.Add(chiendich);
            }
            else
            {
                chiendich.iTongquy += tienQuyenGop;                
            }
            var chienDichdb = db.CHIENDICHes.SingleOrDefault(x => x.MaCD == iMaCD);
            if(chienDichdb != null)
            {
                chienDichdb.TongQuy += (decimal)tienQuyenGop;
                db.SaveChanges();
            }
            return Redirect(strURL);
        }
        //Trang Quyên Góp
        public ActionResult QuyenGop()
        {
            List<DonateVM> lstQuyenGop = layQuyenGop();
            if(lstQuyenGop.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Tongtien = TongTien();
            return View(lstQuyenGop);
        }
        public ActionResult xoaQuyenGop(int iMaCDich)
        {
            List<DonateVM> lstQuyenGop = layQuyenGop();
            //kiểm tra quyengop đã có trong session["QuyenGop"]
            DonateVM quyenGop = lstQuyenGop.SingleOrDefault(x => x.iMaCD == iMaCDich);
            if(quyenGop != null)
            {
                lstQuyenGop.RemoveAll(x => x.iMaCD == iMaCDich);
                return RedirectToAction("QuyenGop");       
            }
            if (lstQuyenGop.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("QuyenGop");
        }
        public ActionResult capNhatQuyenGop(int iMaCDich, FormCollection c)
        {
            List<DonateVM> lstQuyenGop = layQuyenGop();
            DonateVM quyenGop = lstQuyenGop.SingleOrDefault(x => x.iMaCD == iMaCDich);
            if (quyenGop != null)
            {
                quyenGop.iSoTien = double.Parse(c["tienQuyenGop"].ToString());
            }


            return RedirectToAction("QuyenGop");
        }
        public double TongTien()
        {
            double iTongTien = 0;
            List<DonateVM> lstQuyenGop = Session["quyenGop"] as List<DonateVM>;
            if (lstQuyenGop != null)
            {
                iTongTien = lstQuyenGop.Sum(x => x.iSoTien);
            }
            return iTongTien;
        }

        //Thông tin quyên góp
        [HttpGet]
        public ActionResult ThongTinQuyenGop()
        {
            if (Session["TaiKhoan"] == null || string.IsNullOrEmpty(Session["TaiKhoan"].ToString()))
            {
                TempData["ReturnUrl"] = Request.Url.AbsoluteUri;
                return RedirectToAction("DangNhap", "User");
            }

            var lstQuyenGop = layQuyenGop(); // Lấy danh sách từ session
            if (lstQuyenGop == null || !lstQuyenGop.Any())
            {
                return RedirectToAction("Index", "Home"); // Không có dữ liệu quyên góp
            }

            ViewBag.Tongtien = TongTien(); // Gửi tổng tiền
            return View(lstQuyenGop); // Truyền dữ liệu vào View
        }

        [HttpPost]
        public ActionResult ThongTinQuyenGop(FormCollection c)
        {
            if (Session["TaiKhoan"] == null || string.IsNullOrEmpty(Session["TaiKhoan"].ToString()))
            {
                return RedirectToAction("DangNhap", "User");
            }
            List<DonateVM> lstQuyenGop = layQuyenGop();
            if (lstQuyenGop == null || lstQuyenGop.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            QUYENGOP qg = new QUYENGOP();
            NGUOIDUNG nd = (NGUOIDUNG)Session["TaiKhoan"];
            qg.MaND = nd.MaND;
            qg.NgayQuyenGop = DateTime.Now;
            qg.SoTien = (decimal)TongTien(); //tổng tiền
            db.QUYENGOPs.Add(qg);
            db.SaveChanges();
            //chi tiết quyên góp
            foreach( var item in lstQuyenGop)
            {
                CHITIETQUYENGOP ctqg = new CHITIETQUYENGOP();
                ctqg.MaQG = qg.MaQG;
                ctqg.MaCD= item.iMaCD;
                ctqg.SoTienQG = (decimal)item.iSoTien;
                db.CHITIETQUYENGOPs.Add (ctqg);

                var chienDich = db.CHIENDICHes.FirstOrDefault(cd => cd.MaCD == item.iMaCD);
                if (chienDich != null)
                {
                    chienDich.TongQuy += ctqg.SoTienQG;
                    db.SaveChanges(); // Cập nhật vào CSDL sau khi thay đổi
                }

            }
            db.SaveChanges();


            // Kiểm tra loại thanh toán
            var paymentType = int.Parse(c["TypePayment"]);
            if (paymentType == 2) // Thanh toán qua VNPay
            {
                string paymentUrl = UrlPayment(2, qg.MaQG.ToString());
                return Redirect(paymentUrl); // Chuyển hướng người dùng đến VNPay
            }

            Session["quyenGop"] = null;
            return RedirectToAction("XacNhanQuyenGop", "Donate");
        }



        public ActionResult XacNhanQuyenGop()
        {
            return View();
        }



        #region Thanh toán VnPay
        public string UrlPayment(int TypePaymentVN, string iMaQG)
        {
            var urlPayment = "";
            var quyenGop = db.QUYENGOPs.FirstOrDefault(x => x.MaQG.ToString()  == iMaQG);
            //Get Config Info
            string vnp_Returnurl = ConfigurationManager.AppSettings["vnp_Returnurl"]; //URL nhan ket qua tra ve 
            string vnp_Url = ConfigurationManager.AppSettings["vnp_Url"]; //URL thanh toan cua VNPAY 
            string vnp_TmnCode = ConfigurationManager.AppSettings["vnp_TmnCode"]; //Ma định danh merchant kết nối (Terminal Id)
            string vnp_HashSecret = ConfigurationManager.AppSettings["vnp_HashSecret"]; //Secret Key

            //Build URL for VNPAY
            VnPayLibrary vnpay = new VnPayLibrary();
            var Price = (long)TongTien()* 100;
            vnpay.AddRequestData("vnp_Amount", Price.ToString()); //Số tiền thanh toán. Số tiền không mang các ký tự phân tách thập phân, phần nghìn, ký tự tiền tệ. Để gửi số tiền thanh toán là 100,000 VND (một trăm nghìn VNĐ) thì merchant cần nhân thêm 100 lần (khử phần thập phân), sau đó gửi sang VNPAY là: 10000000

            vnpay.AddRequestData("vnp_Version", VnPayLibrary.VERSION);
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            if (TypePaymentVN == 1)
            {
                vnpay.AddRequestData("vnp_BankCode", "VNPAYQR");
            }
            else if (TypePaymentVN == 2)
            {
                vnpay.AddRequestData("vnp_BankCode", "VNBANK");
            }
            else if (TypePaymentVN == 3)
            {
                vnpay.AddRequestData("vnp_BankCode", "INTCARD");
            }

            vnpay.AddRequestData("vnp_CreateDate", (quyenGop.NgayQuyenGop ?? DateTime.Now).ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress());
            vnpay.AddRequestData("vnp_Locale", "vn");
            vnpay.AddRequestData("vnp_OrderInfo", HttpUtility.UrlEncode($"Thanh toán đơn hàng: {quyenGop.MaQG}"));

            vnpay.AddRequestData("vnp_OrderType", "other"); //default value: other

            vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);
            vnpay.AddRequestData("vnp_TxnRef", quyenGop.MaQG.ToString()); // Mã tham chiếu của giao dịch tại hệ thống của merchant. Mã này là duy nhất dùng để phân biệt các đơn hàng gửi sang VNPAY. Không được trùng lặp trong ngày

            urlPayment = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);
            //log.InfoFormat("VNPAY URL: {0}", paymentUrl);
            return urlPayment;
        }
        #endregion
        public ActionResult PaymentCallback()
        {
            var vnp_HashSecret = ConfigurationManager.AppSettings["vnp_HashSecret"];
            VnPayLibrary vnpay = new VnPayLibrary();
            var request = HttpContext.Request.QueryString;
            foreach (string key in request)
            {
                if (!string.IsNullOrEmpty(key))
                {
                    vnpay.AddResponseData(key, request[key]);
                }
            }

            string vnp_SecureHash = Request.QueryString["vnp_SecureHash"];
            bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, vnp_HashSecret);

            if (checkSignature)
            {
                // Xử lý kết quả thanh toán thành công
                string vnp_ResponseCode = Request.QueryString["vnp_ResponseCode"];
                if (vnp_ResponseCode == "00")
                {
                    // Thành công
                    return RedirectToAction("XacNhanQuyenGop");
                }
                else
                {
                    // Lỗi thanh toán
                    return RedirectToAction("QuyenGop");
                }
            }
            else
            {
                // Sai chữ ký
                return RedirectToAction("QuyenGop");
            }
        }

    }
}