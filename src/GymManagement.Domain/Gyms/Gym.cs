namespace GymManagement.Domain.Gyms;

public class Gym
{
  private readonly Guid _subscriptionId;
  private readonly int _maxRooms;
  public Guid Id { get; set; }
  public string Name { get; set; }

  public Gym(string name, Guid subscriptionId, Guid? id = null)
  {
    Name = name;
    _subscriptionId = subscriptionId;
    Id = id ?? Guid.NewGuid();
  }

  private Gym() { }

}