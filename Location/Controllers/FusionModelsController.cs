using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Location.DAL;
using Location.Models;  

using Microsoft.AspNet.Identity;

using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;
namespace Location.Controllers
{
    [Authorize]
    public class FusionModelsController : Controller
    {
        /*
                private ApplicationConnection db = new ApplicationConnection();
                private ApplicationDbContext dbRoles = new ApplicationDbContext();
        */

            /****************************************************
            Fusion des deux conexts mis en commentaire plus hauts
            ****************************************************/

        private LocationContext db = new LocationContext();


        public ActionResult ListUsers()
        {
            var userRoles = new List<RolesViewModel>();
            var context = new ApplicationDbContext();
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            //Get all the usernames
            foreach (var user in userStore.Users)
            {
                var r = new RolesViewModel
                {
                    UserName = user.UserName,
                    Nom = user.Nom,
                    Prenom = user.Prenom,
                    Courriel = user.Email
                };
                userRoles.Add(r);
            }
            return View(userRoles);
        }

        

       
    }
}
