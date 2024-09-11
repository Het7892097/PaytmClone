using Backend_API.DTO;
using Backend_API.Models;

namespace Backend_API.Mappers
{
    public class UserCU_Mapper
    {
        public static User Mapper(User_CUDTO useDetails)
        {
            return new User
            {
                username = useDetails.username,
                firstName = useDetails.firstName,
                lastName = useDetails.lastName,
                password = useDetails.password
            };
        }
    }
}
