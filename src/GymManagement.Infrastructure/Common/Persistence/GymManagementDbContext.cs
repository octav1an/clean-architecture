using System.Reflection;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Gyms;
using GymManagement.Domain.Subscriptions;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.Infrastructure.Common.Persistance;

public class GymManagementDbContext : DbContext, IUnitOfWork
{
  public DbSet<Subscription> Subscriptions { get; set; } = null!;
  public DbSet<Gym> Gyms { get; set; }

  public GymManagementDbContext(DbContextOptions options) : base(options)
  {
  }

  public async Task CommitChangesAsync()
  {
    await base.SaveChangesAsync();
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    base.OnModelCreating(modelBuilder);
  }
}