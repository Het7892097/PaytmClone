namespace Backend_API.Models
{
    public class Account
    {
        public int id { get; set; }
        public int UserId { get; set; }
        public int balance { get; set; } = 0;

        //Navigation property linking this Account table to User table :)
        public  User? User { get; set; }
    }
}
