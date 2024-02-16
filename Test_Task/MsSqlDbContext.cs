using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using Test_Task.Models;

namespace Test_Task;

public class MsSqlDbContext:DbContext
{
    public DbSet<ButtonColorEntity> ButtonExperimentEntities { get; set; }
    public DbSet<PriceEntity> PriceExperimentEntities { get; set; }
    private int DefaultValue = 1;
    public MsSqlDbContext(DbContextOptions<MsSqlDbContext> options) : base(options) {}
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ButtonColorEntity>()
           .Property(e => e.RequestCount).HasDefaultValue(DefaultValue);        // Set default value for request column
        modelBuilder.Entity<PriceEntity>()
           .Property(e => e.RequestCount).HasDefaultValue(DefaultValue);
    }
}
