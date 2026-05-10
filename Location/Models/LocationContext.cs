using System.Data.Entity;
using Location.DAL;

namespace Location.Models
{
    public class LocationContext : ApplicationConnection
    {       
        public virtual DbSet<ApplicationUser> ApplicationUser { get; set; }
        public virtual DbSet<ApplicationConnection> ApplicationConnection { get; set; }
    }
}







