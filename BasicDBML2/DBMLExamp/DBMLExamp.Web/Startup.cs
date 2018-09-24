using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DBMLExamp.Web.Startup))]
namespace DBMLExamp.Web
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
