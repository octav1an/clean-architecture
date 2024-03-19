using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Gyms;
using GymManagement.Infrastructure.Common.Persistance;

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

  public async Task<Gym?> GetByIdAsync(Guid id)
  {
    return await _dbContext.Gyms.FindAsync(id);
  }
}