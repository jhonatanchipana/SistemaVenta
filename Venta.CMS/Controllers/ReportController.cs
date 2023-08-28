using Microsoft.AspNetCore.Mvc;

namespace Venta.CMS.Controllers
{
    public class ReportController : Controller
    {
        public IActionResult ReportInAndOut()
        {
            return View();
        }
    }
}
