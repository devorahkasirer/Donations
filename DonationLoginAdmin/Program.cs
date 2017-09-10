using Donations.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationLoginAdmin
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("First Name:");
            string first = Console.ReadLine();
            Console.WriteLine("Last Name:");
            string last = Console.ReadLine();
            Console.WriteLine("Email:");
            string email = Console.ReadLine();
            Console.WriteLine("Password:");
            string password1 = Console.ReadLine();
            string salt = PasswordHelper.GenerateSalt();
            string hash = PasswordHelper.HashPassword(password1, salt);
            var repo = new DonationRepository(Properties.Settings.Default.ConStr);
            User user = new User
            {
                FirstName = first,
                LastName = last,
                Email = email,
                PasswordSalt = salt,
                PasswordHash = hash,
                isAdmin = true
            };
            repo.AddUser(user);
            Console.WriteLine("Account Created");
            Console.ReadKey(true);
        }
    }
}
