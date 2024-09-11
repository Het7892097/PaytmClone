using System.ComponentModel.DataAnnotations;

namespace Backend_API.DTO
{
    public class UserSignDTO
    {
        //Annotaions are used as we are using this DTO for user interactions

        [Required]
        [EmailAddress]
        public string username { get; set; }=string.Empty;
        [Required]
        [MinLength(4)]
        [MaxLength(40)]
        public string Password { get; set; }=string.Empty;
    }
}
