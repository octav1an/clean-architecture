using GymManagement.Application.Subscriptions.Commands.CreateSubscription;
using GymManagement.Application.Subscriptions.Commands.DeleteSubscription;
using GymManagement.Application.Subscriptions.Queries;
using GymManagement.Contracts.Subscriptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using DomainSubscriptionType = GymManagement.Domain.Subscriptions.SubscriptionType;

namespace GymManagement.Api.Controllers;

[Route("[controller]")]
public class SubscriptionsController : ApiController
{

  private readonly IMediator _mediator;

  public SubscriptionsController(IMediator mediator)
  {
    _mediator = mediator;
  }

  [HttpPost]
  public async Task<IActionResult> CreateSubscription(CreateSubscriptionRequest request)
  {
    if (!DomainSubscriptionType.TryFromName(
      request.SubscriptionType.ToString(),
      out var subscriptionType))
    {
      return Problem(
        statusCode: StatusCodes.Status400BadRequest,
        detail: "Invalid subscription type");
    }

    var command = new CreateSubscriptionCommand(
      subscriptionType,
      request.AdminId);

    var createSubscriptionResult = await _mediator.Send(command);

    return createSubscriptionResult.Match(
      subscription => Ok(new SubscriptionResponse(subscription.Id, request.SubscriptionType)),
      errors => Problem(errors));
  }

  [HttpGet("{subscriptionId:guid}")]
  public async Task<IActionResult> GetSubscription(Guid subscriptionId)
  {
    var query = new GetSubscriptionQuery(subscriptionId);

    var getSubscriptionResult = await _mediator.Send(query);

    return getSubscriptionResult.Match(
      subscription => Ok(new SubscriptionResponse(
        subscription.Id,
        Enum.Parse<SubscriptionType>(subscription.SubscriptionType.Name))),
      errors => Problem(errors)
    );
  }

  [HttpDelete("{subscriptionId:guid}")]
  public async Task<IActionResult> DeleteSubscription(Guid subscriptionId)
  {
    var command = new DeleteSubscriptionCommand(subscriptionId);

    var deleteSubscriptionResult = await _mediator.Send(command);

    return deleteSubscriptionResult.Match(
      _ => NoContent(),
      errors => Problem(errors));
  }
}