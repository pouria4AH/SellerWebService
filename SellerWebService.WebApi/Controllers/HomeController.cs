using Microsoft.AspNetCore.Mvc;

namespace SellerWebService.WebApi.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("/")]
        [HttpGet("/index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
