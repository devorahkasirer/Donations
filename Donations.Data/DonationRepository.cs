using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Donations.Data
{
    public class DonationRepository
    {
        private string _connectionString;
        public DonationRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void AddUser(User user)
        {
            using (var context = new DonationsDataContext(_connectionString))
            {
                context.Users.InsertOnSubmit(user);
                context.SubmitChanges();
            }
        }
        public void AddApplication(Application application)
        {
            using (var context = new DonationsDataContext(_connectionString))
            {
                context.Applications.InsertOnSubmit(application);
                context.SubmitChanges();
            }
        }
        public void AddCategory(Category category)
        {
            using (var context = new DonationsDataContext(_connectionString))
            {
                context.Categories.InsertOnSubmit(category);
                context.SubmitChanges();
            }
        }
        public User GetUserbyId(int userId)
        {
            using (var context = new DonationsDataContext(_connectionString))
            {
                return context.Users.First(u => u.Id == userId);
            }
        }
        public User GetUserbyEmail(string email)
        {
            using (var context = new DonationsDataContext(_connectionString))
            {
                return context.Users.FirstOrDefault(u => u.Email == email);
            }
        }
        public Application GetApplicationbyId(int applicationId)
        {
            using (var context = new DonationsDataContext(_connectionString))
            {
                return context.Applications.First(a => a.Id == applicationId);
            }
        }
        public IEnumerable<Application> GetApplicationsForUserId(int userId)
        {
            using (var context = new DonationsDataContext())
            {
                var loadOptions = new DataLoadOptions();
                loadOptions.LoadWith<Application>(a => a.Category);
                context.LoadOptions = loadOptions;
                return context.Applications.Where(a => a.UserId == userId).ToList();
            }
        }
        public IEnumerable<Category> GetCategories()
        {
            using (var context = new DonationsDataContext(_connectionString))
            {
                return context.Categories.ToList();
            }
        }
        public IEnumerable<Application> GetPending()
        {
            using (var context = new DonationsDataContext())
            {
                var loadOptions = new DataLoadOptions();
                loadOptions.LoadWith<Application>(a => a.Category);
                loadOptions.LoadWith<Application>(a => a.User);
                context.LoadOptions = loadOptions;
                return context.Applications.Where(a => !a.isApproved).ToList();
            }
        }
        public void ApproveApplication(int Id)
        {
            using (var context = new DonationsDataContext())
            {
                context.ExecuteCommand("UPDATE Applications SET isApproved = {0} WHERE Id = {1}", true, Id);
            }
        }
    }
}
