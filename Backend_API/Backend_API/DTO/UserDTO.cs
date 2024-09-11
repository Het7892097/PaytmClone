namespace Backend_API.DTO
{
    public class UserDTO
    {
        //Annotaions are not used as we are not using this DTO for user interactions, but just for filtering password from database to get expose

        public int id { get; set; }
        public string firstName { get; set; } = string.Empty;   
        public string lastName { get; set; }=string.Empty;
        public string username { get; set; } = string.Empty;
    }
}
