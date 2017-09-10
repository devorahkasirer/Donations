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
    [Authorize]
    public class UserController : Controller
    {
        public ActionResult Index()
        {
            var repo = new DonationRepository(Properties.Settings.Default.ConStr);
            var vm = new UserViewModel();
            string email = User.Identity.Name;
            vm.User = repo.GetUserbyEmail(email);
            return View(vm);
        }
        public ActionResult Apply()
        {
            var repo = new DonationRepository(Properties.Settings.Default.ConStr);
            var vm = new ApplicationViewModel();
            string email = User.Identity.Name;
            vm.User = repo.GetUserbyEmail(email);
            vm.Categories = repo.GetCategories();
            return View(vm);
        }
        [HttpPost]
        public ActionResult addApplication(Application application)
        {
            var repo = new DonationRepository(Properties.Settings.Default.ConStr);
            application.Date = DateTime.Now;
            repo.AddApplication(application);
            return Redirect("/home/index");
        }
        public ActionResult History()
        {
            var repo = new DonationRepository(Properties.Settings.Default.ConStr);
            var vm = new HistoryViewModel();
            string email = User.Identity.Name;
            var user = repo.GetUserbyEmail(email);
            vm.User = user;
            vm.Applications = repo.GetApplicationsForUserId(user.Id);
            return View(vm);
        }
        public ActionResult signOut()
        {
            FormsAuthentication.SignOut();
            return Redirect("/home/index");
        }
    }
}