using GymManagement.Application.Admins.Commands;
using GymManagement.Contracts.Admins;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.Api.Controllers;

[Route("[controller]")]
public class AdminsController : ApiController
{
  private readonly IMediator _mediator;

  public AdminsController(IMediator mediator)
  {
    _mediator = mediator;
  }

  [HttpPost]
  public async Task<IActionResult> CreateAdmin()
  {
    var command = new CreateAdminCommand();

    var createResult = await _mediator.Send(command);

    return createResult.Match(
      admin => Ok(new AdminResponse(admin.Id)),
      errors => Problem(errors));
  }
}