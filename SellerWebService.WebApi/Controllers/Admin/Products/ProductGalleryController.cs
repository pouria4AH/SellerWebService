using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SellerWebService.WebApi.Controllers.Admin.Products
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "Admin,Employee")]
    public class ProductGalleryController : ControllerBase
    {
    }
}
