namespace GymManagement.Domain.Gyms;

public class Gym
{
  private readonly int _maxRooms;
  public Guid Id { get; set; }
  public string Name { get; set; }
  public Guid SubscriptionId { get; init; }

  public Gym(string name, Guid subscriptionId, Guid? id = null)
  {
    Name = name;
    SubscriptionId = subscriptionId;
    Id = id ?? Guid.NewGuid();
  }

  private Gym() { }

}