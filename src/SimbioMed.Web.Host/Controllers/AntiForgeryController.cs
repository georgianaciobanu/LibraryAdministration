using Microsoft.AspNetCore.Antiforgery;

namespace SimbioMed.Web.Controllers
{
    public class AntiForgeryController : SimbioMedControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}
