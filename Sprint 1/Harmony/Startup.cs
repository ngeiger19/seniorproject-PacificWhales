using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using Harmony.Models;
using System.Diagnostics;

[assembly: OwinStartupAttribute(typeof(Harmony.Startup))]
namespace Harmony
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUsers();
        }

        // In this method we will create default User roles and Admin user for login    
        private void createRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            // Create a venue owner role   
            if (!roleManager.RoleExists("VenueOwner"))
            {                var role = new IdentityRole();
                role.Name = "VenueOwner";
                roleManager.Create(role);
            }

            // Do we need another role?  i.e. "User"

            // creating Creating Employee role   
            /*
            if (!roleManager.RoleExists("Employee"))
            {
                var role = new IdentityRole();
                role.Name = "Employee";
                roleManager.Create(role);
            }
            */
        }
    }
}
