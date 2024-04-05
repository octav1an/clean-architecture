using ErrorOr;
using GymManagement.Application.Gyms.Commands.CreateGym;
using GymManagement.Application.Gyms.Commands.DeleteGym;
using GymManagement.Application.Gyms.Queries.GetGym;
using GymManagement.Application.Gyms.Queries.ListGyms;
using GymManagement.Contracts.Gyms;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.Api.Controllers;

[Route("subscriptions/{subscriptionId:guid}/gyms")]
public class GymsController : ApiController
{
  private readonly IMediator _mediator;

  public GymsController(IMediator mediator)
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

    return createGymResult.Match(
      gym => Ok(new GymResponse(gym.Id, gym.Name)),
      errors => Problem(errors));
  }

  [HttpDelete("{gymId:guid}")]
  public async Task<IActionResult> DeleteGym(
    Guid subscriptionId,
    Guid gymId)
  {
    var command = new DeleteGymCommand(subscriptionId, gymId);

    var deleteGymResult = await _mediator.Send(command);

    return deleteGymResult.Match(
      _ => NoContent(),
      errors => Problem(errors));
  }

  [HttpGet("{gymId:guid}")]
  public async Task<IActionResult> GetGym(
    Guid subscriptionId,
    Guid gymId)
  {
    var query = new GetGymQuery(subscriptionId, gymId);

    var queryResult = await _mediator.Send(query);

    return queryResult.Match(
      gym => Ok(new GymResponse(gym.Id, gym.Name)),
      errors => Problem(errors));
  }

  [HttpGet]
  public async Task<IActionResult> ListGyms(Guid subscriptionId)
  {
    var query = new ListGymsQuery(subscriptionId);

    var queryResult = await _mediator.Send(query);

    return queryResult.Match(
      gyms => Ok(gyms.ConvertAll(gym => new GymResponse(gym.Id, gym.Name))),
      errors => Problem(errors));
  }


}
