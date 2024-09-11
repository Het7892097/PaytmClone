using Backend_API.DTO;

namespace Backend_API.Interfaces
{
    public interface IAccountRepo
    {
        Task<int> GetBalance(int userId);
        Task<string> TransferMoney(SendMoneyDTO data);
        Task<string> UserAccCreator(int userId, int balance);
    }
}
