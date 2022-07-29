namespace JWTPractice.Models.Authentication
{
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public virtual IList<UserModel> Users { get; set; }
    }
}
