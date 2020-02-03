using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace class_project.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
        /*: base("ClassprojectContext", throwIfV1Schema: false)*/
        : base("ClassprojectContext_Azure", throwIfV1Schema: false)
        {
            // Disable code-first migrations
            Database.SetInitializer<ApplicationDbContext>(null);
        }

            public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<class_project.Models.Athlete> Athletes { get; set; }

        public System.Data.Entity.DbSet<class_project.Models.Coach> Coaches { get; set; }

        public System.Data.Entity.DbSet<class_project.Models.Team> Teams { get; set; }
    }
}