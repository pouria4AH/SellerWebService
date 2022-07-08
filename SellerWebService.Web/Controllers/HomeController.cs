using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace SellerWebService.Web.Controllers
{
    public class HomeController : SiteBaseController
    {

        public IActionResult Index()
        {
            return View();
        }

    }
}