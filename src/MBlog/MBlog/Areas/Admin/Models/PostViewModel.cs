using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MBlog.Areas.Admin.Models
{
    public class PostViewModel
    {
        public string PostId { get; set; }
        public string Title { get; set; }
        public DateTime PublishDate { get; set; }
        public string Content { get; set; }
    }
}