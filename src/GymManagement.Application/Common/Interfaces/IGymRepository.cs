using GymManagement.Domain.Gyms;

namespace GymManagement.Application.Common.Interfaces;

public interface IGymRepository
{
  Task AddGymAsync(Gym gym);
  Task RemoveGymAsync(Gym gym);
  Task RemoveRangeAsync(List<Gym> gyms);
  Task UpdateAsync(Gym gym);
  Task<Gym?> GetByIdAsync(Guid id);
  Task<List<Gym>> ListBySubscriptionIdAsync(Guid subscriptionId);
  Task<bool> ExistsAsync(Guid id);
}