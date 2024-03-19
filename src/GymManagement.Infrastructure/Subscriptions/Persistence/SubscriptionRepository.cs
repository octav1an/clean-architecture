using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Subscriptions;
using GymManagement.Infrastructure.Common.Persistance;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.Infrastructure.Subscriptions.Persistance;

public class SubscriptionsRepository : ISubscriptionsRepository
{
  private readonly GymManagementDbContext _dbContext;

  public SubscriptionsRepository(GymManagementDbContext dbContext)
  {
    _dbContext = dbContext;
  }

  public async Task AddSubscriptionAsync(Subscription subscription)
  {
    await _dbContext.Subscriptions.AddAsync(subscription);
  }

  public Task RemoveSubscriptionAsync(Subscription subscription)
  {
    _dbContext.Remove(subscription);
    return Task.CompletedTask;
  }

  public async Task<Subscription?> GetByIdAsync(Guid subscriptionId)
  {
    return await _dbContext.Subscriptions.FindAsync(subscriptionId);
  }

  public async Task<bool> ExistsAsync(Guid subscriptionId)
  {
    return await _dbContext.Subscriptions
      .AsNoTracking()
      .AnyAsync(subscription => subscription.Id == subscriptionId);
  }
}