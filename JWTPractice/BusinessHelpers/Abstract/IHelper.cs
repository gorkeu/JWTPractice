using JWTPractice.Models.Authentication;

namespace JWTPractice.BusinessHelpers.Abstract
{
    public interface IHelper
    {
        public string Generate(UserModel user);
        public UserModel Authenticate(UserLogin userLogin);
        public UserModel GetCurrentUser(HttpContext context);
        public List<UserModel> GetListOfUsers();
    }
}
