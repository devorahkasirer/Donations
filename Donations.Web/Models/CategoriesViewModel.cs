using Donations.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Donations.Web.Models
{
    public class CategoriesViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
    }
}