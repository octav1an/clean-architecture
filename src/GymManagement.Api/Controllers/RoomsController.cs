using GymManagement.Application.Rooms.Commands.CreateRoom;
using GymManagement.Application.Rooms.Commands.DeleteRoom;
using GymManagement.Application.Rooms.Queries.ListRooms;
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

  [HttpDelete("{roomId:guid}")]
  public async Task<IActionResult> DeleteRoom(
    Guid gymId,
    Guid roomId)
  {
    var command = new DeleteRoomCommand(gymId, roomId);

    var deleteRoomResult = await _mediator.Send(command);

    return deleteRoomResult.MatchFirst<IActionResult>(
      _ => NoContent(),
      _ => Problem());
  }

  [HttpGet]
  public async Task<IActionResult> ListRooms(Guid gymId)
  {
    var query = new ListRoomsQuery(gymId);

    var queryResult = await _mediator.Send(query);

    return queryResult.MatchFirst(
      rooms => Ok(rooms.ConvertAll(room => new RoomResponse(room.Id, room.Name))),
      _ => Problem());
  }
}