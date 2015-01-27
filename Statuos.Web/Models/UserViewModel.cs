using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Statuos.Web.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public bool IsActive { get; set; }
    }
}