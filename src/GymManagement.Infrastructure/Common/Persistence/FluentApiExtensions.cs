using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymManagement.Infrastructure.Common.Persistance;

public static class FluentApiExtensions
{
  public static PropertyBuilder<T> HasListOfIdsConverter<T>(this PropertyBuilder<T> propertyBuilder)
  {
    return propertyBuilder.HasConversion(
      new ListOfIdsConverter(),
      new ListOfIdsComparer());
  }
}
