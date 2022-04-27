using Microsoft.AspNetCore.Mvc;
using SimbioMed.Web.Controllers;

namespace SimbioMed.Web.Public.Controllers
{
    public class AboutController : SimbioMedControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}