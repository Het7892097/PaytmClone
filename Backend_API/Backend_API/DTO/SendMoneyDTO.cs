using System.ComponentModel.DataAnnotations;

namespace Backend_API.DTO
{
    public class SendMoneyDTO
    {
        [Required]
        [Range(1, 10000,ErrorMessage ="Such user id is not possible")]
        public int senderId { get; set; } 
        [Required]
        [Range(1,10000, ErrorMessage = "Such user id is not possible")]
        public int receiverId { get; set; }
        [Required]
        [Range(1,1000000,ErrorMessage ="You can't send money less than 1 and greater than 10 lakh")]//10lakh limit as money-transfer limit on all user
        public int amount { get; set; } 
    }
}
