using Backend_API.DTO;
using Backend_API.Models;

namespace Backend_API.Mappers
{
    public class AccountDTOMapper
    {
        public static AccountDTO Mapper(Account accDetails)
        {
            return new AccountDTO
            {
                UserId = accDetails.UserId,
                balance = accDetails.balance
            };
        }
    }
}
