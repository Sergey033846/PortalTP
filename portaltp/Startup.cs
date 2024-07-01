using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(portaltp.Startup))]
namespace portaltp
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
