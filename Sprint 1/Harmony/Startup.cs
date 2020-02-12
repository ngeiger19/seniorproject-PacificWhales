using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Harmony.Startup))]
namespace Harmony
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
