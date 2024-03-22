using GymManagement.Domain.Gyms;
using ErrorOr;

namespace GymManagement.Domain.Subscriptions;

public class Subscription
{
  private readonly Guid _adminId;
  private readonly List<Guid> _gymIds = new();

  public Guid Id { get; private set; }
  public SubscriptionType SubscriptionType { get; private set; }

  public Subscription(SubscriptionType subscriptionType, Guid adminId, Guid? id = null)
  {
    SubscriptionType = subscriptionType;
    _adminId = adminId;
    Id = id ?? Guid.NewGuid();
  }

  public ErrorOr<Success> AddGym(Gym gym)
  {
    if (_gymIds.Contains(gym.Id))
    {
      throw new Exception("Gym already exists in the subscription");
    }

    if (_gymIds.Count() >= GetMaxGyms())
    {
      return SubscriptionErrors.CannotHaveMoreGymsThanTheSubscriptionAllows;
    }

    _gymIds.Add(gym.Id);

    return Result.Success;
  }

  public void RemoveGym(Guid gymId)
  {
    _gymIds.Remove(gymId);
  }

  private Subscription() { }
}