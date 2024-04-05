using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Gyms;
using MediatR;

namespace GymManagement.Application.Gyms.Commands.CreateGym;

public class CreateGymCommandHandler : IRequestHandler<CreateGymCommand, ErrorOr<Gym>>
{
  private readonly IGymRepository _gymRepository;
  private readonly ISubscriptionsRepository _subscriptionsRepository;
  private readonly IUnitOfWork _unitOfWork;

  public CreateGymCommandHandler(IGymRepository gymRepository, ISubscriptionsRepository subscriptionsRepository, IUnitOfWork unitOfWork)
  {
    _gymRepository = gymRepository;
    _unitOfWork = unitOfWork;
    _subscriptionsRepository = subscriptionsRepository;
  }

  public async Task<ErrorOr<Gym>> Handle(CreateGymCommand request, CancellationToken cancellationToken)
  {
    var subscription = await _subscriptionsRepository.GetByIdAsync(request.SubscriptionId);

    if (subscription is null)
    {
      return Error.NotFound(description: "Subscription not found");
    }

    // Potentially check for authorization here

    var gym = new Gym(
      name: request.Name,
      subscriptionId: request.SubscriptionId,
      maxRooms: subscription.GetMaxRooms());

    var addGymResult = subscription.AddGym(gym);

    if (addGymResult.IsError)
    {
      return addGymResult.Errors;
    }

    await _subscriptionsRepository.UpdateAsync(subscription);
    await _gymRepository.AddGymAsync(gym);
    await _unitOfWork.CommitChangesAsync();

    return gym;
  }
}