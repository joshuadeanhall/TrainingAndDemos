using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KO_Angular_Demo.Startup))]
namespace KO_Angular_Demo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
