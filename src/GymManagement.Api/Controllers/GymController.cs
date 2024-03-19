using GymManagement.Application.Gyms.Commands.CreateGym;
using GymManagement.Application.Gyms.Queries.GetGymQuery;
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
}
