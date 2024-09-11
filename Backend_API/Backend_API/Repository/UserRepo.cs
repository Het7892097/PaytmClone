using Backend_API.Database;
using Backend_API.DTO;
using Backend_API.Interfaces;
using Backend_API.JWT;
using Backend_API.Mappers;
using Backend_API.Migrations;
using Backend_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace Backend_API.Repository
{
    public class UserRepo : IUserRepo
    {
        private readonly ApplicationDBContext _context;
        private readonly IAccountRepo _accRepo;
        private readonly Generator _jwt;
        public UserRepo(ApplicationDBContext context,IAccountRepo accRepo,Generator jwt)
        {
            _context = context;
            _accRepo = accRepo;
            _jwt = jwt;
        }
        public async Task<string> Login(UserSignDTO userDetails)
        {
            var TargetUser = await _context.Users.FirstOrDefaultAsync(user => user.username == userDetails.username && user.password == userDetails.Password);
            if (TargetUser == null) {
                //user does not exist so retuning empty string
                return "UserNotFound";
            }
            else
            {
                //token generation algo
                string token = _jwt.tokenGenerator(UserDTOMapper.Mapper(TargetUser));
                if (token == "") //just confirming for satifaction that the returned token is not empty
                    return "JWTTokenProblem";
                else
                    return token;
            }
        }

        public async Task<string> UserCreator(User_CUDTO userDetails)
        {
            var TargetUser = await _context.Users.FirstOrDefaultAsync(user => user.username == userDetails.username);
            if (TargetUser != null) {
                //user does exist, so Signup not possible
                return "UserExists";
            }
            else
            {
                //User does not exists
                try {
                    var newUser = UserCU_Mapper.Mapper(userDetails);
                    await _context.Users.AddAsync(newUser);
                    await _context.SaveChangesAsync();
                    //User Account creation logic
                    Random randomer=new Random();
                    int randomBalance = randomer.Next(1, 1000000);
                    string result = await _accRepo.UserAccCreator(newUser.id, randomBalance);
                   if(result=="UserAccountCreationFailed")
                    {
                        return result;
                    }
                    //Token generation algo
                    string token=_jwt.tokenGenerator(UserDTOMapper.Mapper(newUser));
                    if (token == "")//just confirming for satifaction that the returned token is not empty
                        return "JWTTokenProblem";
                    else
                        return token;
                }
                catch (Exception e)
                {
                    Console.Write(e.Message.ToString());
                    //If any error occurs during User creation, then it returns DBFail as string
                    return "DBFail";
                }
            }
        }

        public   List<UserDTO> UserFilterer(string? filter)
            //There doesn't seem an requirement to be an async, so turned it into an sync method and also removing Task
            //Also try converting it back into async if required
        {
            //List<User> filteredUserList= _context.Users.Where(user=>user.firstName.ToLower().Contains(filter.ToLower()) || user.lastName.Contains(filter.ToLower())).ToList();
            var UserList = _context.Users;
            if (string.IsNullOrWhiteSpace(filter))
            {
                return UserDTOListMapper.Mapper(UserList.ToList());
            }
            else
            {
                List<UserDTO> filteredList = UserDTOListMapper.Mapper(UserList.Where(user => user.firstName.ToLower().Contains(filter.ToLower()) || user.lastName.ToLower().Contains(filter.ToLower())).ToList());
                return filteredList;
            }
        }
            public async Task<string> UserUpdater(User_CUDTO userDetails)
            {
                var CurrentUser = await _context.Users.FirstOrDefaultAsync(user => user.username == userDetails.username);
                if (CurrentUser == null) {
                    return "UserNotFound";
                }
                else
                {
                    try
                    {
                        CurrentUser.username = userDetails.username;
                        CurrentUser.password = userDetails.password;
                        CurrentUser.firstName = userDetails.firstName;
                        CurrentUser.lastName = userDetails.lastName;
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception e) {
                    Console.WriteLine(e.Message);
                        return "DBFail";
                    }
                    //All checked and Db updation successful
                    return "Done";
                }
            }
        }
    }
