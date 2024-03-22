using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using MediatR;

namespace GymManagement.Application.Subscriptions.Commands.DeleteSubscription;

public class DeleteSubscriptionCommandHandler : IRequestHandler<DeleteSubscriptionCommand, ErrorOr<Deleted>>
{
  private readonly ISubscriptionsRepository _subscriptionRepository;
  private readonly IGymRepository _gymRepository;
  private readonly IUnitOfWork _unitOfWork;

  public DeleteSubscriptionCommandHandler(
    ISubscriptionsRepository subscriptionRepository,
    IGymRepository gymRepository,
    IUnitOfWork unitOfWork)
  {
    _subscriptionRepository = subscriptionRepository;
    _unitOfWork = unitOfWork;
    _gymRepository = gymRepository;
  }

  public async Task<ErrorOr<Deleted>> Handle(DeleteSubscriptionCommand request, CancellationToken cancellationToken)
  {
    var subscription = await _subscriptionRepository.GetByIdAsync(request.subscriptionId);

    if (subscription is null)
    {
      return Error.NotFound(description: "Subscription not found");
    }

    // Remove gyms associated to the subscription
    var gyms = await _gymRepository.ListBySubscriptionIdAsync(subscription.Id);

    await _gymRepository.RemoveRangeAsync(gyms);
    await _subscriptionRepository.RemoveSubscriptionAsync(subscription);
    await _unitOfWork.CommitChangesAsync();

    return Result.Deleted;
  }
}