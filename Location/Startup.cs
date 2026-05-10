using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Location.Models; using Location.DAL;
using Owin;
using System.Security.Claims;

[assembly: OwinStartupAttribute(typeof(Location.Startup))]
namespace Location
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


            // In Startup iam creating first Admin Role and creating a default Admin User 
            if (!roleManager.RoleExists("Admin"))
            {

                // first we create Admin rool
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website				

                var user = new ApplicationUser();
                user.UserName = "Admin";
                user.Email = "rmeziani69gmail.com";
                string userPWD = "Bonjour123!";
                var chkUser = UserManager.Create(user, userPWD);
                //Add default User to Role Admin
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");
                }
            }
            // In Startup iam creating first Admin Role and creating a default Admin User 
            if (!roleManager.RoleExists("Super utilisateur"))
            {
                // first we create Admin rool
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Super utilisateur";
                roleManager.Create(role);
                //Here we create a Admin super user who will maintain the website				
                var user = new ApplicationUser();
                user.UserName = "Super utilisateur";
                user.Email = "rachid_meziani@hotmail.fr";
                string userPWD = "Bonjour123!";
                var chkUser = UserManager.Create(user, userPWD);
                //Add default User to Role Admin
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Super utilisateur");
                }
            }
            // In Startup iam creating first Admin Role and creating a default Admin User 
            if (!roleManager.RoleExists("Utilisateur"))
            {
                // first we create Admin rool
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Utilisateur";
                roleManager.Create(role);
                //Here we create a Admin super user who will maintain the website				
                var user = new ApplicationUser();
                user.UserName = "Utilisateur";
                user.Email = "rachid_meziani@hotmail.ca";
                string userPWD = "Bonjour123!";
                var chkUser = UserManager.Create(user, userPWD);
                //Add default User to Role Admin
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Utilisateur");
                }
            }
            // In Startup iam creating first Admin Role and creating a default Admin User 
            if (!roleManager.RoleExists("Gestionnaire"))
            {
                // first we create Admin rool
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Gestionnaire";
                roleManager.Create(role);
                //Here we create a Admin super user who will maintain the website				
                var user = new ApplicationUser();
                user.UserName = "Gestionnaire";
                user.Email = "rachid_meziani@hotmail.fr";
                string userPWD = "Bonjour123!";
                var chkUser = UserManager.Create(user, userPWD);
                //Add default User to Role Admin
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Gestionnaire");
                }
            }
        }
    }
}
