using JWTPractice.BusinessHelpers.Abstract;
using JWTPractice.Models.Authentication;
using JWTPractice.Models.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace JWTPractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IHelper _helper;
        public UserController(IHelper helper)
        {
            _helper = helper;
        }


        [HttpGet("Admins")]
        [Authorize(Roles = "Admin")]
        public IActionResult AdminsEndpoint()
        {
            var currentUser = _helper.GetCurrentUser(this.HttpContext);

            return Ok($"Hi {currentUser.Name}, you are an {currentUser.Role.RoleName}");


        }

        [HttpGet("Sellers")]
        [Authorize(Roles = "Seller, Admin")]
        public IActionResult SellersEndPoint()
        {
            var currentUser = _helper.GetCurrentUser(this.HttpContext);

            return Ok($"Hello {currentUser.Name}, you are a {currentUser.Role.RoleName}");
            
        }


        [HttpGet("Public")]
        public IActionResult Public()
        {
            return Ok("Hi you're on public property");
        }


    }
}
