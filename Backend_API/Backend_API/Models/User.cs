namespace Backend_API.Models
{
    public class User
    {
        public int id { get; set; }
        public string firstName { get; set; }=string.Empty;
        public string lastName { get; set; }=string.Empty;
        public string username { get; set; }=string.Empty;
        public string password { get; set; }=string.Empty;
    }
}
