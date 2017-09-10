using Donations.Data;
using Donations.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Donations.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if(User.Identity.IsAuthenticated)
            {
                var vm = new UserViewModel();
                var repo = new DonationRepository(Properties.Settings.Default.ConStr);
                string email = User.Identity.Name;
                vm.User = repo.GetUserbyEmail(email);
                if(vm.User.isAdmin)
                {
                    return Redirect("/admin/index");
                }
                return Redirect("/user/index");
            }
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(User user, string password)
        {
            user.PasswordSalt = PasswordHelper.GenerateSalt();
            user.PasswordHash = PasswordHelper.HashPassword(password, user.PasswordSalt);
            user.isAdmin = false;
            var repo = new DonationRepository(Properties.Settings.Default.ConStr);
            repo.AddUser(user);
            FormsAuthentication.SetAuthCookie(user.Email, true);
            var vm = new UserViewModel();
            vm.User = user;
            return Redirect("/user/index");
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            var repo = new DonationRepository(Properties.Settings.Default.ConStr);
            User user = repo.GetUserbyEmail(email);
            if(user == null)
            {
                return View();
            }
            var isMatch = PasswordHelper.PasswordMatch(password, user.PasswordSalt, user.PasswordHash);
            if (isMatch)
            {
                FormsAuthentication.SetAuthCookie(email, true);
                var vm = new UserViewModel();
                vm.User = repo.GetUserbyEmail(email);
                if (vm.User.isAdmin)
                {
                    return Redirect("/admin/index");
                }
                return Redirect("/user/index");
            }
            return View();
        }
    }
}