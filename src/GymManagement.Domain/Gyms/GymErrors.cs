using ErrorOr;

namespace GymManagement.Domain.Gyms;

public static class GymErrors
{
  public static readonly Error CannotHaveMoreRoomsThanTheSubscriptionAllows = Error.Validation(
    code: "Gym.CannotHaveMoreRoomsThanTheSubscriptionAllows",
    description: "A gym cannot have more room than the subscription allows");
}