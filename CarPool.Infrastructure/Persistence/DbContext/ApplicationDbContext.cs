using CarPool.Domain.Trips;
using CarPool.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace CarPool.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;
    private readonly PublishDomainEventsInterceptor _publishDomainEventsInterceptor;

    public DbSet<User> Users { get; set; }
    public DbSet<Trip> Trips { get; set; }

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor
,
        PublishDomainEventsInterceptor publishDomainEventsInterceptor) : base(options)
    {
        _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
        _publishDomainEventsInterceptor = publishDomainEventsInterceptor;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("Persistence");
        modelBuilder.ApplyConfiguration(new TripConfigurations());
        modelBuilder.ApplyConfiguration(new UserConfigurations());

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor, _publishDomainEventsInterceptor);
    }

}