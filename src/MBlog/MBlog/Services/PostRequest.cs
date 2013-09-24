using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceHost;

namespace MBlog.Services
{
    [Route("/posts", "GET")]
    public class PostRequest
    {
        public string Name { get; set; }
    }
}