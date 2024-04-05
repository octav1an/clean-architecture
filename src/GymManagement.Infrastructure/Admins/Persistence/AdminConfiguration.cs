using GymManagement.Domain.Admins;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymManagement.Infrastructure.Admins.Persistance;

public class AdminConfiguration : IEntityTypeConfiguration<Admin>
{
  public void Configure(EntityTypeBuilder<Admin> builder)
  {
    builder.HasKey(a => a.Id);

    builder.Property(a => a.Id)
      .ValueGeneratedNever();

    builder.Property(a => a.SubscriptionId);
  }
}