using JWTPractice.BusinessHelpers.Abstract;
using JWTPractice.Models.Authentication;
using JWTPractice.Models.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTPractice.BusinessHelpers
{
    public class Helper : IHelper
    {
        private readonly IConfiguration _config;
        
        public Helper(IConfiguration config)
        {
            _config = config;
            
        }
        public List<UserModel> GetListOfUsers()
        {
            using (MyDbContext db = new MyDbContext())
            {
                var list = db.Users.Include("Role").ToList();
                return list;
            }
        }
        public string Generate(UserModel user)
        {
            using (MyDbContext db = new MyDbContext())
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(ClaimTypes.Email, user.EmailAddress),
                new Claim(ClaimTypes.GivenName, user.Name),
                new Claim(ClaimTypes.Surname, user.Surname),
                new Claim(ClaimTypes.Role, user.Role.RoleName),
                
            };
                var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                    _config["Jwt:Audience"],
                    claims,
                    expires: DateTime.Now.AddMinutes(15),
                    signingCredentials: credentials);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
        }

        public UserModel Authenticate(UserLogin userLogin)
        {
            using (MyDbContext db = new MyDbContext())
            {
                var currentUser = db.Users.Include("Role").FirstOrDefault(x => x.UserName.ToLower() == userLogin.Username.ToLower() && x.Password == userLogin.Password);
                    

                if (currentUser != null)
                {
                    return currentUser;
                }
                return null;
            }

        }
        public UserModel GetCurrentUser(HttpContext context)
        {
            using (MyDbContext db = new MyDbContext())
            {
                var identity = context.User.Identity as ClaimsIdentity;

                if (identity != null)
                {
                    var userClaims = identity.Claims;

                    var username = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;

                    var user = db.Users.Include("Role").FirstOrDefault(x => x.UserName == username);

                    return user;
                }
                return null;
            }

        }
    }
}
