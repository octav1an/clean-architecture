using ErrorOr;
using MediatR;

namespace GymManagement.Application.Subscriptions.Commands.DeleteSubscription;

public record DeleteSubscriptionCommand(Guid subscriptionId) : IRequest<ErrorOr<Deleted>>;