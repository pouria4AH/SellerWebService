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

        [HttpGet("bank-data")]
        public async Task<ActionResult> GetBankData()
        {
            try
            {
                var data = await _storeService.GetBankData(User.GetStoreCode());
                if(data == null) return BadRequest();
                return Ok(data);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

    }
}
