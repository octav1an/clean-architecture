using System.Runtime.CompilerServices;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Admins;
using GymManagement.Infrastructure.Common.Persistance;

namespace GymManagement.Infrastructure.Admins.Persistance;

public class AdminRepository : IAdminRepository
{
  private readonly GymManagementDbContext _dbContext;

  public AdminRepository(GymManagementDbContext dbContext)
  {
    _dbContext = dbContext;
  }

  public async Task AddAdminAsync(Admin admin)
  {
    await _dbContext.AddAsync(admin);
  }

  public async Task<Admin?> GetByIdAsync(Guid adminId)
  {
    return await _dbContext.Admins.FindAsync(adminId);
  }

  public Task UpdateAsync(Admin admin)
  {
    _dbContext.Update(admin);
    return Task.CompletedTask;
  }
}