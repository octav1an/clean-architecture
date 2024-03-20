using GymManagement.Application.Gyms.Commands.CreateGym;
using GymManagement.Application.Gyms.Commands.DeleteGym;
using GymManagement.Application.Gyms.Queries.GetGym;
using GymManagement.Application.Gyms.Queries.ListGyms;
using GymManagement.Contracts.Gyms;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.Api.Controllers;

[ApiController]
[Route("subscriptions/{subscriptionId:guid}/gyms")]
public class GymController : ControllerBase
{
  private readonly IMediator _mediator;

  public GymController(IMediator mediator)
  {
    _mediator = mediator;
  }

  [HttpPost]
  public async Task<IActionResult> CreateGym(
    CreateGymRequest request,
    Guid subscriptionId)
  {
    var command = new CreateGymCommand(subscriptionId, request.Name);

    var createGymResult = await _mediator.Send(command);

    return createGymResult.MatchFirst(
      gym => Ok(new GymResponse(gym.Id, gym.Name)),
      _ => Problem());
  }

  [HttpDelete("{gymId:guid}")]
  public async Task<IActionResult> DeleteGym(
    Guid subscriptionId,
    Guid gymId)
  {
    var command = new DeleteGymCommand(subscriptionId, gymId);

    var deleteGymResult = await _mediator.Send(command);

    return deleteGymResult.MatchFirst<IActionResult>(
      _ => NoContent(),
      err => Problem(
        statusCode: StatusCodes.Status404NotFound,
        detail: err.Description));
  }

  [HttpGet("{gymId:guid}")]
  public async Task<IActionResult> GetGym(
    Guid subscriptionId,
    Guid gymId)
  {
    var query = new GetGymQuery(subscriptionId, gymId);

    var queryResult = await _mediator.Send(query);

    return queryResult.MatchFirst(
      gym => Ok(new GymResponse(gym.Id, gym.Name)),
      err => Problem(
        statusCode: StatusCodes.Status404NotFound,
        detail: err.Description));
  }

  [HttpGet]
  public async Task<IActionResult> ListGyms(Guid subscriptionId)
  {
    var query = new ListGymsQuery(subscriptionId);

    var queryResult = await _mediator.Send(query);

    return queryResult.MatchFirst(
      gyms => Ok(gyms.ConvertAll(gym => new GymResponse(gym.Id, gym.Name))),
      _ => Problem());
  }
}
