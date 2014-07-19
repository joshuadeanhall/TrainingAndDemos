using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KO_Angular_Demo.Models
{
    public class ProjectViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Effort { get; set; }
        public float Cost { get; set; }
        public string UserName { get; set; }
    }
}