using Microsoft.AspNetCore.Mvc;

namespace SellerWebService.Web.Areas.Seller.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
