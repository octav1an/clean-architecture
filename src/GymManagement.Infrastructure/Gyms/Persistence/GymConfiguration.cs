using GymManagement.Domain.Gyms;
using GymManagement.Infrastructure.Common.Persistance;
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

    builder.Property(g => g.SubscriptionId);

    builder.Property<int>("_maxRooms")
      .HasColumnName("MaxRooms");

    builder.Property<List<Guid>>("_roomIds")
      .HasColumnName("RoomIds")
      .HasListOfIdsConverter();
  }
}
