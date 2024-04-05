using GymManagement.Domain.Rooms;

namespace GymManagement.Application.Common.Interfaces;

public interface IRoomRepository
{
  Task AddRoomAsync(Room room);
  Task<Room?> GeByIdAsync(Guid id);
  Task RemoveRoomAsync(Room room);
  Task RemoveRangeAsync(List<Room> rooms);
  Task<List<Room>> ListByGymIdAsync(Guid gymId);
}