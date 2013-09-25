using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceHost;

namespace MBlog.Services
{
    [Route("/posts", "GET")]
    public class Posts
    {
        public string Name { get; set; }
    }

    [Route("/posts/{Id}")]
    public class GetPost
    {
        public string Id { get; set; }
    }

}