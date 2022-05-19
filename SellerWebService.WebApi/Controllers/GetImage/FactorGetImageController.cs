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
        /// <summary>
        /// گرفتن عکس امضا
        /// </summary>
        /// <param name="imageName"></param>
        /// <remarks>send image name and return 200 our 400 if is 200 have bite array </remarks>
        /// <response code="200">have bite array is image</response>
        /// <response code="400">have non data</response>
        [HttpGet("signature/{imageName}")]
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
        /// <summary>
        /// گرفتن عکس لوگو
        /// </summary>
        /// <param name="imageName"></param>
        /// <remarks>send image name and return 200 our 400 if is 200 have bite array </remarks>
        /// <response code="200">have bite array is image</response>
        /// <response code="400">have non data</response>
        [HttpGet("logo/{imageName}")]
        public async Task<ActionResult<byte[]>> GetLogoImage(string imageName)
        {
            try
            {
                var path = Path.Combine(_env.WebRootPath, PathExtension.StoreDetailsLogoImage, imageName);
                var fileBytes = await System.IO.File.ReadAllBytesAsync(path);
                return Ok(fileBytes);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
        /// <summary>
        /// گرفتن عکس امضا
        /// </summary>
        /// <param name="imageName"></param>
        /// <remarks>send image name and return 200 our 400 if is 200 have bite array </remarks>
        /// <response code="200">have bite array is image</response>
        /// <response code="400">have non data</response>
        [HttpGet("stamp/{imageName}")]
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
