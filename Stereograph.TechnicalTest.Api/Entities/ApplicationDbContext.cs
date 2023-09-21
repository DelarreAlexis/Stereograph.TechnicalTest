using Microsoft.EntityFrameworkCore;
namespace Stereograph.TechnicalTest.Api.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<PersonFollow>()
            .HasKey(pf => new { pf.PersonId, pf.FollowingId });

        modelBuilder.Entity<PersonFollow>()
            .HasOne(pf => pf.Person)
            .WithMany(p => p.Followers)
            .HasForeignKey(pf => pf.PersonId);

        modelBuilder.Entity<PersonFollow>()
            .HasOne(pf => pf.Following)
            .WithMany(p => p.Followings)
            .HasForeignKey(pf => pf.FollowingId);
    }

    public DbSet<Person> Persons { get; set; }
    public DbSet<PersonFollow> PersonFollows { get; set; }
}
