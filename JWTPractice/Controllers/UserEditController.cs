using JWTPractice.BusinessHelpers.Abstract;
using JWTPractice.Models.Authentication;
using JWTPractice.Models.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWTPractice.Controllers
{
    
    public class UserEditController : Controller
    {
        private readonly IHelper _helper;
        public UserEditController(IHelper helper)
        {
            _helper = helper;
        }

        [Authorize]
        public IActionResult Index()
        {

            var list = _helper.GetListOfUsers();
            return View(list);
        }

        [Authorize]
        public IActionResult CreateUser()
        {
            using (MyDbContext db = new MyDbContext())
            {
                var roles = db.Roles.ToList();
                ViewBag.Roles = roles;
            }

            return View();
        }
        [HttpPost]
        [Authorize]
        public IActionResult CreateUser(UserModel userModel)
        {
            using (MyDbContext db = new MyDbContext())
            {
                db.Users.Add(userModel);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
            
        }

        public IActionResult Login()
        {
            return View();
        }
    }
}
