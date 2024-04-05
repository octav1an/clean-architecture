using GymManagement.Domain.Subscriptions;

namespace GymManagement.Domain.Admins;

public class Admin
{
  public Guid Id { get; private set; }
  public Guid? SubscriptionId { get; private set; }

  public Admin(Guid? id = null, Guid? subscriptionId = null)
  {
    Id = id ?? Guid.NewGuid();
    SubscriptionId = subscriptionId;
  }

  public void SetSubscription(Subscription subscription)
  {
    SubscriptionId = subscription.Id;
  }

  public void DeleteSubscription(Guid subscriptionId)
  {
    if (SubscriptionId != subscriptionId)
    {
      throw new Exception();
    }

    SubscriptionId = null;
  }

  public Admin() { }
}