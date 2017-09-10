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
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            var repo = new DonationRepository(Properties.Settings.Default.ConStr);
            var vm = new UserViewModel();
            string email = User.Identity.Name;
            vm.User = repo.GetUserbyEmail(email);
            return View(vm);
        }
        public ActionResult Pending()
        {
            return View();
        }
        public ActionResult allPending()
        {
            var repo = new DonationRepository(Properties.Settings.Default.ConStr);
            var Pending = repo.GetPending();
            return Json(Pending.Select(a => new {
                id = a.Id,
                date = a.Date.ToShortDateString(),
                userName = a.User.FirstName + " " + a.User.LastName,
                userEmail = a.User.Email,
                amount = a.Amount,
                categoryName = a.Category.Name,
                description = a.Description
            }), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public void approve(int Id)
        {
            var repo = new DonationRepository(Properties.Settings.Default.ConStr);
            repo.ApproveApplication(Id);
        }
        public ActionResult Categories()
        {
            var repo = new DonationRepository(Properties.Settings.Default.ConStr);
            var vm = new CategoriesViewModel();
            vm.Categories = repo.GetCategories();
            return View(vm);
        }
        [HttpPost]
        public ActionResult addCategory(Category category)
        {
            var repo = new DonationRepository(Properties.Settings.Default.ConStr);
            repo.AddCategory(category);
            return Json(new {
                name = category.Name
            });
        }
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return Redirect("/home/index");
        }
    }
}