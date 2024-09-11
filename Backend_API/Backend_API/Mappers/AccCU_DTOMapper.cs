using Backend_API.Models;

namespace Backend_API.Mappers
{
    public class AccCU_DTOMapper
    {
        public static Account Mapper(int userId,int balance)
        {
            return new Account
            {
                UserId = userId,
                balance = balance
            };
        }
    }
}
