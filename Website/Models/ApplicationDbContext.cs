using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Website.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Bootcamp> Bootcamps { get; set; }
        public DbSet<BootCampSession> Sessions { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}