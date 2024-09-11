using Backend_API.DTO;
using Backend_API.Models;

namespace Backend_API.Mappers
{
    public class UserDTOListMapper
    {
        public static List<UserDTO> Mapper(List<User> users) {
            List<UserDTO> resultList=new List<UserDTO>();

            foreach (User user in users) { 
                UserDTO iUser=UserDTOMapper.Mapper(user);
                resultList.Add(iUser);
            }
            return resultList;
        }
    }
}
