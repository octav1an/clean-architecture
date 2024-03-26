using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Rooms;
using GymManagement.Infrastructure.Common.Persistance;

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

  public Task<List<Room>> ListByGymIdAsync(Guid gymId)
  {
    throw new NotImplementedException();
  }

  public Task RemoveRangeAsync(List<Room> rooms)
  {
    throw new NotImplementedException();
  }

  public Task RemoveRoomAsync(Room room)
  {
    throw new NotImplementedException();
  }
}