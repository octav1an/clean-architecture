using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Subscriptions;
using MediatR;

namespace GymManagement.Application.Subscriptions.Commands.CreateSubscription;

public class CreateSubscriptionCommandHandler : IRequestHandler<CreateSubscriptionCommand, ErrorOr<Subscription>>
{
  private readonly ISubscriptionsRepository _subscriptionsRepository;
  private readonly IUnitOfWork _unitOfWork;

  public CreateSubscriptionCommandHandler(ISubscriptionsRepository subscriptionRepository, IUnitOfWork unitOfWork)
  {
    _subscriptionsRepository = subscriptionRepository;
    _unitOfWork = unitOfWork;
  }

  public async Task<ErrorOr<Subscription>> Handle(CreateSubscriptionCommand request, CancellationToken cancellationToken)
  {
    // Create a subscription
    var subscription = new Subscription(
      subscriptionType: request.SubscriptionType,
      adminId: request.AdminId);

    // Add it to the database
    await _subscriptionsRepository.AddSubscriptionAsync(subscription);
    await _unitOfWork.CommitChangesAsync();

    return subscription;
  }
}