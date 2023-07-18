using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class WebServiceController : Controller
    {
        [HttpGet]
        public ActionResult Consultar()
        {
            return View();
        }
    }
}
