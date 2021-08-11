using Microsoft.AspNetCore.Mvc;

namespace WorkPortal.Controllers
{
    public class PayslipController : Controller
    {
        public IActionResult All()
        {
            return View();
        }
    }
}
