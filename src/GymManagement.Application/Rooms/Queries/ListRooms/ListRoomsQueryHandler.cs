using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Rooms;
using MediatR;

namespace GymManagement.Application.Rooms.Queries.ListRooms;

public class ListRoomsQueryHandler : IRequestHandler<ListRoomsQuery, ErrorOr<List<Room>>>
{
  private readonly IRoomRepository _roomRepository;
  private readonly IGymRepository _gymRepository;

  public ListRoomsQueryHandler(IRoomRepository roomRepository, IGymRepository gymRepository)
  {
    _roomRepository = roomRepository;
    _gymRepository = gymRepository;
  }

  public async Task<ErrorOr<List<Room>>> Handle(ListRoomsQuery request, CancellationToken cancellationToken)
  {
    if (!await _gymRepository.ExistsAsync(request.GymId))
    {
      return Error.NotFound(description: "Gym not found");
    }

    return await _roomRepository.ListByGymIdAsync(request.GymId);
  }
}