using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
namespace Location.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        //custom fields
        [StringLength(100)]
        public string Nom { get; set; }
        [StringLength(100)]
        public string Prenom { get; set; }
        [Required(ErrorMessage = "Le champ {0} est obligatoire")]
        [StringLength(100)]
        public override string Email { get; set; }
        [Required(ErrorMessage = "Le champ {0} est obligatoire")]
        [StringLength(100)]
        public override string UserName { get; set; }
        [StringLength(50)]
        public override string PhoneNumber { get; set; }
        //flag for user status
        //public bool IsActive { get; set; }
       

        // ajout pour affichage nom et prénom de l'usager au lieu du nom d'usager connecté Houda 2018-12-06
        public string FullName => $"{Nom} {Prenom}";

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
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        //public System.Data.Entity.DbSet<Location.DAL.Immeubles> Immeubles { get; set; }
    }
}