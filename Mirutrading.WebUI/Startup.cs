using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Mirutrading.WebUI.Startup))]
namespace Mirutrading.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
