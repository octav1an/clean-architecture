using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Gyms;
using MediatR;

namespace GymManagement.Application.Gyms.Queries.GetGym;

public class GetGymQueryHandler : IRequestHandler<GetGymQuery, ErrorOr<Gym>>
{
  private readonly IGymRepository _gymRepository;
  private readonly ISubscriptionsRepository _subscriptionRepository;

  public GetGymQueryHandler(IGymRepository gymRepository, ISubscriptionsRepository subscriptionRepository)
  {
    _gymRepository = gymRepository;
    _subscriptionRepository = subscriptionRepository;
  }

  public async Task<ErrorOr<Gym>> Handle(GetGymQuery request, CancellationToken cancellationToken)
  {
    if (!await _subscriptionRepository.ExistsAsync(request.SubscriptionId))
    {
      return Error.NotFound(description: "Subscription not found");
    }

    var gym = await _gymRepository.GetByIdAsync(request.GymId);

    return gym is null
      ? Error.NotFound(description: "Gym not found")
      : gym;
  }
}