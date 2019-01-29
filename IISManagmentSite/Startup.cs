using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IISManagementSite.Startup))]
namespace IISManagementSite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
