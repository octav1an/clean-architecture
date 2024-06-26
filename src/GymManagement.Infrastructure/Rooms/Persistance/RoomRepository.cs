using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Rooms;
using GymManagement.Infrastructure.Common.Persistance;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.Infrastructure.Rooms.Persistence;

public class RoomRepository : IRoomRepository
{
  private readonly GymManagementDbContext _dbContext;

  public RoomRepository(GymManagementDbContext dbContext)
  {
    _dbContext = dbContext;
  }

  public async Task AddRoomAsync(Room room)
  {
    await _dbContext.AddAsync(room);
  }

  public async Task<Room?> GeByIdAsync(Guid id)
  {
    return await _dbContext.Rooms.FindAsync(id);
  }

  public async Task<List<Room>> ListByGymIdAsync(Guid gymId)
  {
    return await _dbContext.Rooms
      .Where(r => r.GymId == gymId)
      .ToListAsync();
  }

  public Task RemoveRangeAsync(List<Room> rooms)
  {
    _dbContext.RemoveRange(rooms);
    return Task.CompletedTask;
  }

  public Task RemoveRoomAsync(Room room)
  {
    _dbContext.Remove(room);
    return Task.CompletedTask;
  }
}