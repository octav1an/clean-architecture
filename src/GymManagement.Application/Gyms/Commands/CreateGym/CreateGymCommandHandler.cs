using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Gyms;
using MediatR;

namespace GymManagement.Application.Gyms.Commands.CreateGym;

public class CreateGymCommandHandler : IRequestHandler<CreateGymCommand, ErrorOr<Gym>>
{
  private readonly IGymRepository _gymRepository;
  private readonly IUnitOfWork _unitOfWork;

  public CreateGymCommandHandler(IGymRepository gymRepository, IUnitOfWork unitOfWork)
  {
    _gymRepository = gymRepository;
    _unitOfWork = unitOfWork;
  }

  public async Task<ErrorOr<Gym>> Handle(CreateGymCommand request, CancellationToken cancellationToken)
  {
    var gym = new Gym(request.Name, request.SubscriptionId);

    await _gymRepository.AddGymAsync(gym);
    await _unitOfWork.CommitChangesAsync();

    return gym;
  }
}