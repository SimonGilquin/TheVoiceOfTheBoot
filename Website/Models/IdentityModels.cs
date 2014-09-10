using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Website.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
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

    public class BootCampSession
    {
        public int Id { get; set; }
        public int BootcampId { get; set; }
        public Bootcamp Bootcamp { get; set; }
        public string Hour { get; set; }
        public string Title { get; set; }
        public string Speakers { get; set; }
        public string Description { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }

    public class Bootcamp
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public bool Current { get; set; }
        public ICollection<BootCampSession> Sessions { get; set; }
    }

    public class Comment
    {
        public int Id { get; set; }
        public int SessionId { get; set; }
        public BootCampSession Session { get; set; }
        [Range(0, 4)]
        public int SpeakerNote { get; set; }
        [Range(0, 4)]
        public int SessionContentNote { get; set; }
        [Range(0, 4)]
        public int SupportNote { get; set; }
        public string CommentText { get; set; }
        public string ImageUrl { get; set; }

        public string UserLogin { get; set; }
    }
}