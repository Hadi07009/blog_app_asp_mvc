using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BootstrapERP.Startup))]
namespace BootstrapERP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
