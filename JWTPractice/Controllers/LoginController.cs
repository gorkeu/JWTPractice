using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using JWTPractice.BusinessHelpers;
using JWTPractice.Models.Authentication;
using JWTPractice.BusinessHelpers.Abstract;

namespace JWTPractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IHelper _helper;
        public LoginController(IHelper helper)
        {
            _helper = helper;
        }
       

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody]UserLogin userLogin)
        {

            var user = _helper.Authenticate(userLogin);

            if (user != null)
            {
                var token = _helper.Generate(user);
                Ok(token);
                RedirectToAction("Index", "UserEdit");
            }
            return NotFound("User not found.");
            
        }
    }
}
