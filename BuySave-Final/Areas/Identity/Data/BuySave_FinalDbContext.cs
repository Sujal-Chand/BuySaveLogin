using BuySave_Final.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BuySave_Final.Models;

namespace BuySave_Final.Areas.Identity.Data;

public class BuySave_FinalDbContext : IdentityDbContext<User>
{
    public BuySave_FinalDbContext(DbContextOptions<BuySave_FinalDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
        builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
    }

    public DbSet<BuySave_Final.Models.Catagory>? Catagory { get; set; }

    public DbSet<BuySave_Final.Models.Country>? Country { get; set; }

    public DbSet<BuySave_Final.Models.BUser>? BUser { get; set; }

    public DbSet<BuySave_Final.Models.Product>? Product { get; set; }

    public DbSet<BuySave_Final.Models.Review>? Review { get; set; }
}

public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.BUsername).HasMaxLength(20);
    }
}