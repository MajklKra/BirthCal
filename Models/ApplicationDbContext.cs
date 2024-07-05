using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace BirthCal.Models
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options) {}
        public DbSet<Person> People { get; set; }
        public DbSet<Present> Presents { get; set; }
        public DbSet<Event> Events { get; set; }
    }
}
