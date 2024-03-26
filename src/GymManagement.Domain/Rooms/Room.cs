namespace GymManagement.Domain.Rooms;

public class Room
{
  public Guid Id { get; }

  public string Name { get; }
  public Guid GymId { get; }

  public Room(Guid gymId, string name, Guid? id = null)
  {
    GymId = gymId;
    Name = name;
    Id = id ?? Guid.NewGuid();
  }

  public Room() { }
}