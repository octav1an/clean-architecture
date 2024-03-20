using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Gyms;
using MediatR;

namespace GymManagement.Application.Gyms.Commands.DeleteGym;

public class DeleteGymCommandHandler : IRequestHandler<DeleteGymCommand, ErrorOr<Deleted>>
{
  private readonly IGymRepository _gymRepository;
  private readonly ISubscriptionsRepository _subscriptionRepository;
  private readonly IUnitOfWork _unitOfWork;

  public DeleteGymCommandHandler(
    IGymRepository gymRepository,
    ISubscriptionsRepository subscriptionRepository,
    IUnitOfWork unitOfWork)
  {
    _gymRepository = gymRepository;
    _subscriptionRepository = subscriptionRepository;
    _unitOfWork = unitOfWork;
  }

  public async Task<ErrorOr<Deleted>> Handle(DeleteGymCommand request, CancellationToken cancellationToken)
  {
    var gym = await _gymRepository.GetByIdAsync(request.GymId);

    if (gym is null)
    {
      return Error.NotFound(description: "Gym not found");
    }

    var subscription = await _subscriptionRepository.GetByIdAsync(request.SubscriptionId);

    if (subscription is null)
    {
      return Error.NotFound(description: "Subscription not found");
    }

    subscription.RemoveGym(gym.Id);

    await _subscriptionRepository.UpdateAsync(subscription);
    await _gymRepository.RemoveGymAsync(gym);
    await _unitOfWork.CommitChangesAsync();

    return Result.Deleted;
  }
}