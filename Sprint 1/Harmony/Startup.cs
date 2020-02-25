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

<<<<<<< HEAD
            
            // Create a venue owner role   
=======
>>>>>>> dev
            if (!roleManager.RoleExists("VenueOwner"))
            {                
                var role = new IdentityRole();
                role.Name = "VenueOwner";
                roleManager.Create(role);
            }

            // Create a general user role   
            if (!roleManager.RoleExists("GeneralUser"))
            {
                var role = new IdentityRole();
                role.Name = "GeneralUser";
                roleManager.Create(role);
            }

            // Do we need another role?  i.e. "User"

            // Create a Musician role   

            if (!roleManager.RoleExists("Musician"))
            {
                var role = new IdentityRole();
                role.Name = "Musician";
                roleManager.Create(role);
            }

        }
    }
}
