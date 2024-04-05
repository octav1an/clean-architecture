using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using MediatR;

namespace GymManagement.Application.Rooms.Commands.DeleteRoom;

public class DeleteRoomCommandHandler : IRequestHandler<DeleteRoomCommand, ErrorOr<Deleted>>
{
  private readonly IRoomRepository _roomRepository;
  private readonly IGymRepository _gymRepository;
  private readonly IUnitOfWork _unitOfWork;

  public DeleteRoomCommandHandler(IRoomRepository roomRepository, IGymRepository gymRepository, IUnitOfWork unitOfWork)
  {
    _roomRepository = roomRepository;
    _gymRepository = gymRepository;
    _unitOfWork = unitOfWork;
  }

  public async Task<ErrorOr<Deleted>> Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
  {
    var gym = await _gymRepository.GetByIdAsync(request.GymId);

    if (gym is null)
    {
      return Error.NotFound(description: "Gym not found");
    }

    var room = await _roomRepository.GeByIdAsync(request.RoomId);

    if (room is null)
    {
      return Error.NotFound(description: "Room not found");
    }

    gym.RemoveRoom(room.Id);

    await _gymRepository.UpdateAsync(gym);
    await _roomRepository.RemoveRoomAsync(room);
    await _unitOfWork.CommitChangesAsync();

    return Result.Deleted;
  }
}