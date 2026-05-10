using System.Data.Entity;
namespace Location.Models
{
    public class LocationContext : ApplicationConnection
    {       
        public virtual DbSet<ApplicationUser> ApplicationUser { get; set; }
        public virtual DbSet<ApplicationConnection> ApplicationConnection { get; set; }
    }
}







