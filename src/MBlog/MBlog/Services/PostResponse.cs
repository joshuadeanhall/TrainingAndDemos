﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MBlog.Services
{
    public class PostResponse
    {
        public string PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublishedOn { get; set; }
    }
}