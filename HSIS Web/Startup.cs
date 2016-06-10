using HSIS_Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HSIS_Web.Startup))]
namespace HSIS_Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesandUsers();
        }

        private void CreateRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!roleManager.RoleExists("Admin"))
            {

                // first we create Admin rool   
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("Vendor"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole {Name = "Vendor"};
                roleManager.Create(role);

            }
            
            if (!roleManager.RoleExists("Assistant"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole {Name = "Assisntant"};
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("Client"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole { Name = "Client" };
                roleManager.Create(role);
            }
        }
    }
}
