using AirlinesReservationSystem.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AirlinesReservationSystem.Startup))]
namespace AirlinesReservationSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateUserAndRole();
        }

        public void CreateUserAndRole()
        {
            _ApplicationDbContext context = new _ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!roleManager.RoleExists("Admin"))
            {

                //Create Admin Role  
                var role = new IdentityRole();
                role.Name = "Admin";              
                roleManager.Create(role);

                var user = new ApplicationUser();
                user.UserName = "Admin";
                user.Email = "XuanHung@gmail.com";

                string pwd = "Hung@123456";

                var checkUser = UserManager.Create(user, pwd);

                //Add default User to Role Admin   
                if (checkUser.Succeeded)
                {
                    var result = UserManager.AddToRole(user.Id, "Admin");
                }
            }

            if (!roleManager.RoleExists("Manager"))
            {

                //Create Manager Role  
                var role = new IdentityRole();
                role.Name = "Manager";
                roleManager.Create(role);
            }
        }
    }
}
