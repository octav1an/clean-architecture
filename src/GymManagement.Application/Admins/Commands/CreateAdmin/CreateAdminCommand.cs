using ErrorOr;
using GymManagement.Domain.Admins;
using MediatR;

namespace GymManagement.Application.Admins.Commands;

public record CreateAdminCommand() : IRequest<ErrorOr<Admin>>;