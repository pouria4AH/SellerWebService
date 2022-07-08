using Microsoft.AspNetCore.Mvc;
using SellerWebService.Web.Models;
using System.Diagnostics;

namespace SellerWebService.Web.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

    }
}