using GymManagement.Domain.Rooms;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.Infrastructure.Rooms.Persistence;

public class RoomConfiguration : IEntityTypeConfiguration<Room>
{
  public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Room> builder)
  {
    builder.HasKey(r => r.Id);

    builder.Property(r => r.Id)
      .ValueGeneratedNever();

    builder.Property(r => r.Name);

    builder.Property(r => r.GymId);
  }
}