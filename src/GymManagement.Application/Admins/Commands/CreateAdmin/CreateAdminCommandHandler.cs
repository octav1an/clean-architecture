using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Admins;
using MediatR;

namespace GymManagement.Application.Admins.Commands;

public class CreateAdminCommandHandler : IRequestHandler<CreateAdminCommand, ErrorOr<Admin>>
{
  private readonly IAdminRepository _adminRepository;
  private readonly IUnitOfWork _unitOfWork;

  public CreateAdminCommandHandler(IAdminRepository adminRepository, IUnitOfWork unitOfWork)
  {
    _adminRepository = adminRepository;
    _unitOfWork = unitOfWork;
  }

  public async Task<ErrorOr<Admin>> Handle(CreateAdminCommand request, CancellationToken cancellationToken)
  {
    var admin = new Admin(id: null);

    await _adminRepository.AddAdminAsync(admin);
    await _unitOfWork.CommitChangesAsync();

    return admin;
  }
}