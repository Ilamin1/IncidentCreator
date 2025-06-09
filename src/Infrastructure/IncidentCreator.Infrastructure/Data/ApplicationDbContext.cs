using IncidentCreator.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IncidentCreator.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Incident> Incidents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Account>()
                .HasIndex(a => a.Name)
                .IsUnique();

            modelBuilder.Entity<Contact>()
                .HasIndex(c => c.Email)
                .IsUnique();

            modelBuilder.Entity<Account>()
                .HasMany(a => a.Incidents)
                .WithOne(i => i.Account)
                .HasForeignKey(i => i.AccountId);

            modelBuilder.Entity<Account>()
                .HasMany(a => a.Contacts)
                .WithOne(c => c.Account)
                .HasForeignKey(c => c.AccountId);
        }
    }
}