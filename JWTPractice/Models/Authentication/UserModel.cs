using System.ComponentModel.DataAnnotations.Schema;

namespace JWTPractice.Models.Authentication
{
    public class UserModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string EmailAddress { get; set; }
        
        public virtual Role Role { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

    }
}
