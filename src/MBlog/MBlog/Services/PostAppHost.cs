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
        }
    }
}