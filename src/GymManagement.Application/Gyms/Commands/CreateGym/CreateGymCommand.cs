using ErrorOr;
using GymManagement.Domain.Gyms;
using MediatR;

namespace GymManagement.Application.Gyms.Commands.CreateGym;

public record CreateGymCommand(Guid SubscriptionId, string Name) : IRequest<ErrorOr<Gym>>;