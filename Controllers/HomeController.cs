using Bay_LapTop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.Common;
using System.Diagnostics;

namespace Bay_LapTop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public QlgiaiBongDaContext db = new QlgiaiBongDaContext();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        /* - Hiển thị danh sách các trận đấu của câu lạc bộ là 101 -*/ 
        public IActionResult Index()
        {
            ViewBag.TenCauThu = db.Cauthus.Take(7).ToList();
            /*- Hiển thị các trận đấu của các câu lạc bộ có mã la 101-*/
            /*- var list_TranDau = db.Trandaus.Where(x => x.Clbkhach=="101" || x.Clbnha=="101").ToList();-*/

            var list_TranDau = db.Trandaus.ToList();


            /*- Hiển thị list cầu thủ ở câu lạc bộ Arsenal => mã 104 -*/
            var list_CauThu = db.Cauthus.Where(x => x.CauLacBoId == "104").Take(7).ToList();

            /*- Hiển thị các huấn luyện viên của các câu lạc bộ -*/
            var list_HLV = db.Huanluyenviens.ToList();
            return View(list_TranDau);
            
        }




        [Route("ThemTranDauMoi")]
        [HttpGet]
        public IActionResult ThemTranDauMoi()
        {
            // Lấy danh sách CLB từ bảng CAULACBO cho CLB khách và CLB nhà
            ViewBag.Clbkhach = new SelectList(db.Caulacbos.ToList(), "CauLacBoId", "TenClb");
            ViewBag.Clbnha = new SelectList(db.Caulacbos.ToList(), "CauLacBoId", "TenClb");

            // Lấy danh sách sân vận động từ bảng SANVANDONG
            ViewBag.SanVanDongId = new SelectList(db.Sanvandongs.ToList(), "SanVanDongId", "TenSan");
            ViewBag.TenCauThu = db.Cauthus.Take(7).ToList();
            return View();
        }
        [Route("ThemTranDauMoi")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemTranDauMoi(Trandau TranDau)
        {
            /*- == true thì chứng tỏ dứ liệu truyền từ form vào hợp lệ -*/
            if (ModelState.IsValid)
            {
                db.Add(TranDau);
                db.SaveChanges();
                return RedirectToAction("Index"); // Chuyển hướng về trang danh sách
            }
            ViewBag.TenCauThu = db.Cauthus.Take(7).ToList();
            return View();
        }



        [Route("SuaTranDau")]
        /*- Tạo một cái trang đưa dữ liệu lên - */
        [HttpGet]
        public IActionResult SuaTranDau(String maTranDau)
        {
            // Lấy danh sách CLB từ bảng CAULACBO cho CLB khách và CLB nhà
            ViewBag.Clbkhach = new SelectList(db.Caulacbos.ToList(), "CauLacBoId", "TenClb");
            ViewBag.Clbnha = new SelectList(db.Caulacbos.ToList(), "CauLacBoId", "TenClb");

            ViewBag.TenCauThu = db.Cauthus.Take(7).ToList();
            var TranDau = db.Trandaus.Find(maTranDau);          
            return View(TranDau);
        }
        [Route("SuaTranDau")]
        /*- Tạo một cái trang đưa dữ liệu lên - */
        [HttpPost]
        /* - Kiểm tra dữ liệu nhập vào có chính xác với quy định về validation không  -*/
        [ValidateAntiForgeryToken]
        public IActionResult SuaTranDau(Trandau TranDau)
        {
            if (ModelState.IsValid)
            {
                db.Update(TranDau);
                db.SaveChanges();
                /* - SAU KHI TẠO MỚI XONG SẼ CHUYỂN SANG DANH MụC SẢN PHẨM -*/
                return RedirectToAction("Index", "Home");
            }
            ViewBag.TenCauThu = db.Cauthus.Take(7).ToList();
            /* - NẾU NHẬP KHÔNG CHÍNH XÁC THÌ CÓ THỂ NHẬP LẠI ĐỂ TẠO CÁI VIEW CHO HỌ CÓ THỂ NHẬP LẠI -*/
            return View(TranDau);
        }





        /*- Xóa sách thì chúng ta sẽ phải cần mã sách -*/
        [Route("XoaTranDau")]
        [HttpGet]
        public IActionResult XoaTranDau(string trandauID)
        {
            TempData["Message"] = "";
            var list_TranDau = db.Trandaus.ToList();
            ViewBag.TenCauThu = db.Cauthus.Take(7).ToList();
            // Kiểm tra chi tiết sản phẩm trước khi xóa
            var chiTietTranDauTonTai = db.TrandauGhibans.Any(x => x.TranDauId == trandauID);
            if (chiTietTranDauTonTai)
            {
                TempData["Message"] = "Không xóa được sản phẩm này vì có chi tiết sản phẩm liên quan.";
                return RedirectToAction("Index", "Home");
            }

            // Xóa các ảnh liên quan đến sản phẩm
            var trongtai_trandau = db.TrongtaiTrandaus.Where(x => x.TranDauId == trandauID);
            if (trongtai_trandau.Any())
            {
                db.RemoveRange(trongtai_trandau);
            }

            var cauthu_trandau = db.TrandauCauthus.Where(x => x.TranDauId == trandauID);
            if (cauthu_trandau.Any())
            {
                db.RemoveRange(cauthu_trandau);
            }

            // Xóa sản phẩm trong bảng TDanhMucSps
            var TranDau = db.Trandaus.Find(trandauID);
            if (TranDau != null)
            {
                db.Remove(TranDau);
                db.SaveChanges();
                TempData["Message"] = "Sản phẩm đã được xóa.";
            }
            else
            {
                TempData["Message"] = "Sản phẩm không tồn tại.";
            }

            return RedirectToAction("Index", "Home");
        }



    }
}
