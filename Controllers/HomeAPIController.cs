using Bay_LapTop.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bay_LapTop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeAPIController : ControllerBase
    {
        public QlgiaiBongDaContext db = new QlgiaiBongDaContext();
        [HttpGet("{cauthuID}")]
        public IEnumerable<Trandau> LayTranDauTheoCauThu(string cauthuID)
        {
            var list_tranDau = db.Trandaus
                .Where(td => db.TrandauCauthus
                    .Any(tdct => tdct.CauThuId == cauthuID && tdct.TranDauId == td.TranDauId))
                .ToList();

            return list_tranDau;
        }
    }
}
