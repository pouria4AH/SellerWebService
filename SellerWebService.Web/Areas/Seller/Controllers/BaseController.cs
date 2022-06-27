using Microsoft.AspNetCore.Mvc;

namespace SellerWebService.Web.Areas.Seller.Controllers
{
    [Area("Seller")]
    [Route("seller")]
    public class BaseController : Controller
    {
        protected string ErrorMessage = "ErrorMessage";
        protected string InfoMessage = "InfoMessage";
        protected string SuccessMessage = "SuccessMessage";
        protected string WarningMessage = "WarningMessage";
    }
}
