using Backend_API.DTO;
using Backend_API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend_API.Controllers
{
    [ApiController]
    [Route("api/v1/account")]
    public class AccountController:ControllerBase
    {
        private readonly IAccountRepo _accRepo;

        public AccountController(IAccountRepo accRepo)
        {
            _accRepo = accRepo;
        }
        [Authorize,HttpGet("Balance")]
        public async Task<IActionResult> BalanceGetter(int userId)
        {
            if (userId <= 0)
            {
                return BadRequest("Such user id is not possible");
            }
            else
            {
                int result=await _accRepo.GetBalance(userId);
                if (result == -1)
                {
                    return NotFound("Such user with provided user-id does not exists");
                }
                else
                {
                    return Ok("Your balance is " + result);
                }
            }
        }
        [Authorize, HttpPost("transfer")]
        public async Task<IActionResult> MoneySender(SendMoneyDTO data)
        {
            string result = await _accRepo.TransferMoney(data);
            if (result == "UserNotFound")
            {
                return NotFound("User does not exists");
            }
            else if (result == "LowBalance")
            {
                return BadRequest("Low balance, you don't have such balance");
            }
            else if(result=="FailTransfer")
            {
                return BadRequest("Transferring of money failed, also don't worry the money is not deducted from your account");
            }
            else
            {
                Console.WriteLine(result);  
                return Ok(result);
            }
        }
    }
}
