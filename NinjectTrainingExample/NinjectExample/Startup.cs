using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NinjectExample.Startup))]
namespace NinjectExample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
