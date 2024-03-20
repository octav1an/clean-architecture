using GymManagement.Domain.Subscriptions;
using GymManagement.Infrastructure.Common.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymManagement.Infrastructure.Subscriptions.Persistance;

public class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
{
  public void Configure(EntityTypeBuilder<Subscription> builder)
  {
    builder.HasKey(s => s.Id);

    builder.Property(s => s.Id)
      .ValueGeneratedNever();

    builder.Property("_adminId")
      .HasColumnName("AdminId");

    builder.Property(s => s.SubscriptionType)
      .HasConversion(
        subscriptionType => subscriptionType.Name,
        name => SubscriptionType.FromName(name, false));

    builder.Property<List<Guid>>("_gymIds")
      .HasColumnName("GymIds")
      .HasListOfIdsConverter();
  }
}