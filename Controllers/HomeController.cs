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

            var list_TranDau = db.Trandaus.Where(x => x.Clbkhach=="101" || x.Clbnha=="101").Take(7).ToList();
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

    }
}
