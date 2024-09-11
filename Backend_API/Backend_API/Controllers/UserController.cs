using Backend_API.DTO;
using Backend_API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend_API.Controllers
{
    [ApiController]
    [Route("api/v1/user")]
    public class UserController:ControllerBase
    {
        private readonly IUserRepo _userRepo;
       
        public UserController(IUserRepo userRepo)
        {
            _userRepo=userRepo;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SigninUp(User_CUDTO userDetails)
        {
            //keep in mind to tune response in accordance to frontend requirements 
            Console.WriteLine("Through signup route");

            string result = await _userRepo.UserCreator(userDetails);
            if (result == "UserExists")
            {
                return BadRequest("User already Exists so try signing in");
            }
            else if (result == "DBFail")
            {
                return BadRequest("User Creation failed due to some server problem");
            }
            else if (result == "UserAccountCreationFailed")
            {
                return BadRequest("User Account creation failed");
            }
            else if (result == "JWTTokenProblem")
            {
                return BadRequest("Some problem occcured while generating an JWT token, try contacting the developer");
            }
            else
            {
                return Ok(new 
                {
                    message="User successfully created",
                    token=result
                });

            }
        }

        [HttpPost("signin")]
        public async Task<IActionResult> Signer(UserSignDTO userDetails)
        {
            Console.WriteLine("Through signin route");

            string result =await _userRepo.Login(userDetails);
            if (result == "UserNotFound")
            {
                return NotFound("User does not exist so try Siging up");
            }
            else
            {
                return Ok(new {
                message="User exists with this provided token",
                        token=result
                });
            }
        }
        [Authorize]
        [HttpPut("/update")]
        public async Task<IActionResult> Updater(User_CUDTO userDetails)
        {
            string result=await _userRepo.UserUpdater(userDetails);
            if(result == "UserNotFound")
            {
                return NotFound("User does not exists so try signing up");
            }
            else if(result == "DBFail")
            {
                return BadRequest("User updation failed due to some server problems");
            }
            else
            {
                return Ok("Updation successful");
            }
        }
        [Authorize]
        [HttpGet("GetUser")]
        public IActionResult UserGetter([FromQuery] string? filter)
        {
            List<UserDTO> userList= _userRepo.UserFilterer(filter);
            if(userList == null)
            { return BadRequest("Such Users not found"); }
            else
            {
                return Ok(userList);
               
            }
        }
       
    }
}
