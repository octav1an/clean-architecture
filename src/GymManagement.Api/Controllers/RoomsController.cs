using GymManagement.Application.Rooms.Commands.CreateRoom;
using GymManagement.Contracts.Rooms;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.Api.Controllers;

[ApiController]
[Route("gyms/{gymId:guid}/rooms")]
public class RoomsController : ControllerBase
{
  private readonly IMediator _mediator;

  public RoomsController(IMediator mediator)
  {
    _mediator = mediator;
  }

  [HttpPost]
  public async Task<IActionResult> CreateRoom(
    CreateRoomRequest request,
    Guid gymId
  )
  {
    var command = new CreateRoomCommand(gymId, request.Name);

    var createRoomResult = await _mediator.Send(command);

    return createRoomResult.MatchFirst(
      room => Ok(new RoomResponse(room.Id, room.Name)),
      _ => Problem());
  }

}