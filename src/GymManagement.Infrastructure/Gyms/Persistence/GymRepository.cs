using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Gyms;
using GymManagement.Infrastructure.Common.Persistance;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.Infrastructure.Gyms.Persistance;

public class GymRepository : IGymRepository
{
  private readonly GymManagementDbContext _dbContext;

  public GymRepository(GymManagementDbContext dbContext)
  {
    _dbContext = dbContext;
  }

  public async Task AddGymAsync(Gym gym)
  {
    await _dbContext.Gyms.AddAsync(gym);
  }

  public async Task<bool> ExistsAsync(Guid id)
  {
    return await _dbContext.Gyms.AsNoTracking().AnyAsync(gym => gym.Id == id);
  }

  public async Task<Gym?> GetByIdAsync(Guid id)
  {
    return await _dbContext.Gyms.FindAsync(id);
  }

  public async Task<List<Gym>> ListBySubscriptionIdAsync(Guid subscriptionId)
  {
    return await _dbContext.Gyms
      .Where(gym => gym.SubscriptionId == subscriptionId)
      .ToListAsync();
  }

  public Task RemoveGymAsync(Gym gym)
  {
    _dbContext.Remove(gym);
    return Task.CompletedTask;
  }

  public Task RemoveRangeAsync(List<Gym> gyms)
  {
    _dbContext.RemoveRange(gyms);
    return Task.CompletedTask;
  }

  public Task UpdateAsync(Gym gym)
  {
    _dbContext.Update(gym);

    return Task.CompletedTask;
  }
}