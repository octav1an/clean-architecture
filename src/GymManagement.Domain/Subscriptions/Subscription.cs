using GymManagement.Domain.Gyms;
using ErrorOr;

namespace GymManagement.Domain.Subscriptions;

public class Subscription
{
  private readonly List<Guid> _gymIds = new();

  public Guid Id { get; private set; }
  public SubscriptionType SubscriptionType { get; private set; }
  public Guid AdminId { get; }

  public Subscription(SubscriptionType subscriptionType, Guid adminId, Guid? id = null)
  {
    SubscriptionType = subscriptionType;
    AdminId = adminId;
    Id = id ?? Guid.NewGuid();
  }

  public int GetMaxGyms() => SubscriptionType.Name switch
  {
    nameof(SubscriptionType.Free) => 1,
    nameof(SubscriptionType.Starter) => 1,
    nameof(SubscriptionType.Pro) => 3,
    _ => throw new InvalidOperationException()
  };

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