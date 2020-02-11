using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(cd_test.Startup))]
namespace cd_test
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
