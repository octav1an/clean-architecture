using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Rooms;
using MediatR;

namespace GymManagement.Application.Subscriptions.Commands.DeleteSubscription;

public class DeleteSubscriptionCommandHandler : IRequestHandler<DeleteSubscriptionCommand, ErrorOr<Deleted>>
{
  private readonly ISubscriptionsRepository _subscriptionRepository;
  private readonly IGymRepository _gymRepository;
  private readonly IAdminRepository _adminRepository;
  private readonly IRoomRepository _roomRepository;
  private readonly IUnitOfWork _unitOfWork;

  public DeleteSubscriptionCommandHandler(
    ISubscriptionsRepository subscriptionRepository,
    IGymRepository gymRepository,
    IRoomRepository roomRepository,
    IAdminRepository adminRepository,
    IUnitOfWork unitOfWork)
  {
    _subscriptionRepository = subscriptionRepository;
    _gymRepository = gymRepository;
    _roomRepository = roomRepository;
    _adminRepository = adminRepository;
    _unitOfWork = unitOfWork;
  }

  public async Task<ErrorOr<Deleted>> Handle(DeleteSubscriptionCommand request, CancellationToken cancellationToken)
  {

    var subscription = await _subscriptionRepository.GetByIdAsync(request.subscriptionId);

    if (subscription is null)
    {
      return Error.NotFound(description: "Subscription not found");
    }

    var admin = await _adminRepository.GetByIdAsync(subscription.AdminId);

    if (admin is null)
    {
      return Error.Unexpected(description: "Admin not found");
    }

    if (admin.SubscriptionId != subscription.Id)
    {
      return Error.Unexpected(description: "Admin has different subscription");
    }

    admin.DeleteSubscription(request.subscriptionId);

    // Remove gyms associated to the subscription
    var gyms = await _gymRepository.ListBySubscriptionIdAsync(subscription.Id);

    // Remove rooms associated with the subscription's gyms
    var rooms = new List<Room>();
    gyms.ForEach(async gym => rooms.AddRange(await _roomRepository.ListByGymIdAsync(gym.Id)));

    await _gymRepository.RemoveRangeAsync(gyms);
    await _roomRepository.RemoveRangeAsync(rooms);
    await _adminRepository.UpdateAsync(admin);
    await _subscriptionRepository.RemoveSubscriptionAsync(subscription);
    await _unitOfWork.CommitChangesAsync();

    return Result.Deleted;
  }
}