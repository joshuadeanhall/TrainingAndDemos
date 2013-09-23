using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Funq;
using ServiceStack.WebHost.Endpoints;

namespace MBlog.Services
{
    public class PostAppHost : AppHostBase
    {
        public PostAppHost() : base("Post Web Services", typeof(PostService).Assembly)
        {
        }

        public override void Configure(Container container)
        {
            Routes.Add<PostRequest>("/post")
                .Add<PostRequest>("/post/{Name}");
        }
    }
}