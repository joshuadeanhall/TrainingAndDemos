using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MBlog.Areas.Admin.Models
{
    public class SettingViewModel
    {
        public string SettingId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}