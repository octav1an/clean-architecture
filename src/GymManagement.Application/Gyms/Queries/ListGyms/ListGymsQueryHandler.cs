using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Application.Gyms.Queries.ListGyms;
using GymManagement.Domain.Gyms;
using MediatR;

namespace GymManagement.Application.Gyms.Queries.GetGym;

public class ListGymsQueryHandler : IRequestHandler<ListGymsQuery, ErrorOr<List<Gym>>>
{
  private readonly IGymRepository _gymRepository;

  public ListGymsQueryHandler(IGymRepository gymRepository)
  {
    _gymRepository = gymRepository;
  }

  public async Task<ErrorOr<List<Gym>>> Handle(ListGymsQuery request, CancellationToken cancellationToken)
  {
    return await _gymRepository.ListBySubscriptionIdAsync(request.SubscriptionId);
  }
}