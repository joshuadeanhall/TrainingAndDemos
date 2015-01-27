using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WhyDirectModelBindingIsBad.Models;

namespace WhyDirectModelBindingIsBad
{
    public class TestContext : DbContext
    {
        public DbSet<User> User { get; set; }
    }
}