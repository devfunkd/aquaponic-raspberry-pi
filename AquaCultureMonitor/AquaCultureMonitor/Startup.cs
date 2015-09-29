using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AquaCultureMonitor.Startup))]
namespace AquaCultureMonitor
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
			app.MapSignalR();
            ConfigureAuth(app);
        }
    }
}
