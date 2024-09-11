using System.ComponentModel.DataAnnotations;

namespace Backend_API.DTO
{
    public class User_CUDTO
    {
        //Annotaions are used as we are using this DTO for user interactions
        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string firstName { get; set; } =string.Empty;
        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string lastName { get; set; } =string.Empty;
        [Required]
        [EmailAddress]
        public string username { get; set; }=string.Empty;
        [Required]
        [MinLength(3)]
        [MaxLength(40)]
        public string password { get; set; }=string.Empty;
    }
}
