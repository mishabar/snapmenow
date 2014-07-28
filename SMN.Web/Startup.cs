using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SMN.Web.Startup))]
namespace SMN.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
