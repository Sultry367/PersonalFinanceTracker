using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PersonalFinanceTracker.Models;

namespace PersonalFinanceTracker.Data;

public class AppDbContext:IdentityDbContext<ApplicationUser,ApplicationRole,string>
{
    public AppDbContext(DbContextOptions<AppDbContext> options):base(options) {}
    
    public DbSet<Transactions> Transaction { get; set; }
    public DbSet<Users> User { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Users>()
            .HasMany(u => u.Transactions)
            .WithOne(t => t.User)
            .HasForeignKey(y => y.UserId)
            .IsRequired();
    }
}