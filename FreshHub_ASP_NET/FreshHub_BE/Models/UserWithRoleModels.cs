namespace FreshHub_BE.Models
{
    public class UserWithRoleModels
    {     
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string[] Roles { get; set; }
        public string PhoneNumber { get; set; }
    };
}
