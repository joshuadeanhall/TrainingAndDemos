using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Neo4jExample.Startup))]
namespace Neo4jExample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
