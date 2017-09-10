using Donations.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Donations.Web.Models
{
    public class ApplicationViewModel
    {
        public User User { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}