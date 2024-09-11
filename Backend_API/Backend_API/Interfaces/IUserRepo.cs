using Backend_API.DTO;

namespace Backend_API.Interfaces
{
    public interface IUserRepo
    {
        Task<string> Login(UserSignDTO userDetails);
        Task<string> UserCreator(User_CUDTO userDetails);
        Task<string> UserUpdater(User_CUDTO userDetails);
        List<UserDTO> UserFilterer(string filter);
    }
}

