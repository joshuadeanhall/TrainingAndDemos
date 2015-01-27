using System;
using System.Web.Mvc;

namespace MBlog.Models
{
    public class PostViewModel
    {
        public string PostId { get; set; }
        public string Title { get; set; }
        public DateTime PublishDate { get; set; }
        [AllowHtml]
        public string Content { get; set; }
    }
}