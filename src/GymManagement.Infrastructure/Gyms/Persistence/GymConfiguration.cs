using GymManagement.Domain.Gyms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymManagement.Infrastructure.Gyms.Persistance;

public class GymConfiguration : IEntityTypeConfiguration<Gym>
{
  public void Configure(EntityTypeBuilder<Gym> builder)
  {
    builder.HasKey(g => g.Id);

    builder.Property(g => g.Id)
      .ValueGeneratedNever();

    builder.Property(g => g.Name);

    builder.Property("_subscriptionId")
      .HasColumnName("SubscriptionId");
  }
}
