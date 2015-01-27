using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TestAngular.Startup))]
namespace TestAngular
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
