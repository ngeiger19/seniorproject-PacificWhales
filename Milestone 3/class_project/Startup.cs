using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(class_project.Startup))]
namespace class_project
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
