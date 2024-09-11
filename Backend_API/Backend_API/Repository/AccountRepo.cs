using Backend_API.Database;
using Backend_API.DTO;
using Backend_API.Interfaces;
using Backend_API.Mappers;
using Backend_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend_API.Repository
{    
    public class AccountRepo : IAccountRepo
    {
        private readonly ApplicationDBContext _context;

        public AccountRepo(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<string> UserAccCreator(int userId, int balance)
        {
            try
            {
                await _context.Accounts.AddAsync(AccCU_DTOMapper.Mapper(userId, balance));
                await _context.SaveChangesAsync();
                return "UserAccountCreated";
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
                return "UserAccountCreationFailed";
            }
        }

        public async Task<int> GetBalance(int userId)
        {
            var TargetUserAcc=await _context.Accounts.FirstOrDefaultAsync(userAcc=> userAcc.UserId == userId);
            if (TargetUserAcc == null) {
                return -1;
            }
            else
            {
                return TargetUserAcc.balance;
            }
        }

        public async Task<string> TransferMoney(SendMoneyDTO data)
        {
            var senderUserAcc=await _context.Accounts.FirstOrDefaultAsync(userAcc=>userAcc.UserId == data.senderId);

            //Manually implement Transaction for atomicity, if not provided by default by EF
            var receiverUserAcc=await _context.Accounts.FirstOrDefaultAsync(receiverUserAcc=>receiverUserAcc.UserId == data.receiverId);

            if (senderUserAcc == null || receiverUserAcc == null)
            {
                return "UserNotFound";
            }
            else {
                var Transaction=await _context.Database.BeginTransactionAsync();
                //Transfer logic 
                if (senderUserAcc.balance < data.amount)
                {
                    return "LowBalance";
                }
                else
                {
                    try
                    {
                        senderUserAcc.balance -= data.amount;
                        receiverUserAcc.balance += data.amount;
                        await _context.SaveChangesAsync();
                        await Transaction.CommitAsync();
                        return "Sucess and updated Sender balance is: "+senderUserAcc.balance+" and Receiver balance is: "+receiverUserAcc.balance;
                    }
                    catch (Exception ex) { 
                     Console.WriteLine(ex.Message);
                        await Transaction.RollbackAsync();
                        return "FailTransfer";
                    }
                }
            }
        }
    }
}
