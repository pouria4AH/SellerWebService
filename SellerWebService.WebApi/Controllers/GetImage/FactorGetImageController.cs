using _0_framework.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SellerWebService.WebApi.Controllers.GetImage
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class FactorGetImageController : ControllerBase
    {
        #region ctor
        private readonly IWebHostEnvironment _env;
        public FactorGetImageController(IWebHostEnvironment env)
        {
            _env = env;
        }

        #endregion

        [HttpGet("signature-image-{imageName}")]
        public async Task<ActionResult<byte[]>> GetSignatureImage(string imageName)
        {
            try
            {
                var path = Path.Combine(_env.WebRootPath, PathExtension.StoreDetailsSignatureImage, imageName);
                var fileBytes = await System.IO.File.ReadAllBytesAsync(path);
                return Ok(fileBytes);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        } 
        
        [HttpGet("stamp-image-{imageName}")]
        public async Task<ActionResult<byte[]>> GetStampImage(string imageName)
        {
            try
            {
                var path = Path.Combine(_env.WebRootPath, PathExtension.StoreDetailsStampImage, imageName);
                var fileBytes = await System.IO.File.ReadAllBytesAsync(path);
                return Ok(fileBytes);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}
