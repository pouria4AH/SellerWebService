using _0_framework.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SellerWebService.Application.interfaces;
using SellerWebService.DataLayer.DTOs.Store;
using SellerWebService.WebApi.Extensions;

namespace SellerWebService.WebApi.Controllers.Store
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = AccountRole.Seller)]
    public class BankManagementController : ControllerBase
    {
        #region ctor

        private readonly IStoreService _storeService;

        public BankManagementController(IStoreService storeService)
        {
            _storeService = storeService;
        }

        #endregion
        /// <summary>
        /// ساخت اصلاعات بانکی 
        /// </summary>
        /// <remarks> just return 400 our 200 by non object. you can have just one and end that you only can edit that</remarks>
        /// <param name="data">data bank</param>
        [HttpPost("bank-data")]
        public async Task<ActionResult> CreateBankData([FromBody] BankDataDto data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var res = await _storeService.CreateBankData(data, User.GetStoreCode());
                    if (res) return Ok();
                    return BadRequest();
                }

                return BadRequest();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
        /// <summary>
        /// ادیت اصلاعات بانکی 
        /// </summary>
        /// <remarks>you can have just one and end that you only can edit that just return 400 our 200 by non object</remarks>
        /// <param name="data">data bank</param>
        [HttpPut("bank-data")]
        public async Task<ActionResult> EditBankData([FromBody] BankDataDto data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var res = await _storeService.EditBankData(data, User.GetStoreCode());
                    if (res) return Ok();
                    return BadRequest();
                }

                return BadRequest();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
        /// <summary>
        /// گرفتن اصلاعات بانکی 
        /// </summary>
        /// <remarks>you can have just one and end that you only can edit that just return 400 our 200 if is 200 have bank data dto</remarks>
        /// <param name="data">data bank</param>
        [HttpGet("bank-data")]
        public async Task<ActionResult<BankDataDto>> GetBankData()
        {
            try
            {
                var data = await _storeService.GetBankData(User.GetStoreCode());
                if (data == null) return BadRequest();
                return Ok(data);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

    }
}
