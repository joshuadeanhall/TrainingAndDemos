using Funq;
using ServiceStack.WebHost.Endpoints;

namespace MBlog.Services
{
    public class PostAppHost : AppHostBase
    {
        public PostAppHost() : base("Blog Web Services", typeof(PostService).Assembly)
        {
        }

        public override void Configure(Container container)
        {
        }
    }
}