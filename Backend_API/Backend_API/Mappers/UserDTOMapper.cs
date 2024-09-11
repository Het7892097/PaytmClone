using Backend_API.DTO;
using Backend_API.Models;

namespace Backend_API.Mappers
{
    public class UserDTOMapper
    {
        public static UserDTO Mapper(User userData)
            {
            return new UserDTO
            {
                id = userData.id,
                username = userData.username,
                firstName = userData.firstName,
                lastName = userData.lastName,
            };
        }
    }
}
