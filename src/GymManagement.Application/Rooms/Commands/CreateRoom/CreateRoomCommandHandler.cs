using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Rooms;
using MediatR;

namespace GymManagement.Application.Rooms.Commands.CreateRoom;

public class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, ErrorOr<Room>>
{
  private readonly IRoomRepository _roomRepository;
  private readonly IGymRepository _gymRepository;
  private readonly IUnitOfWork _unitOfWork;

  public CreateRoomCommandHandler(IRoomRepository roomRepository, IGymRepository gymRepository, IUnitOfWork unitOfWork)
  {
    _roomRepository = roomRepository;
    _gymRepository = gymRepository;
    _unitOfWork = unitOfWork;
  }


  public async Task<ErrorOr<Room>> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
  {
    var gym = await _gymRepository.GetByIdAsync(request.GymId);

    if (gym is null)
    {
      return Error.NotFound(description: "Gym not found");
    }

    var room = new Room(request.GymId, request.Name);

    var addRoomResult = gym.AddRoom(room);

    if (addRoomResult.IsError)
    {
      return addRoomResult.Errors;
    }

    await _gymRepository.UpdateAsync(gym);
    await _roomRepository.AddRoomAsync(room);
    await _unitOfWork.CommitChangesAsync();

    return room;
  }
}