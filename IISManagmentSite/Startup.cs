using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IISManagmentSite.Startup))]
namespace IISManagmentSite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
