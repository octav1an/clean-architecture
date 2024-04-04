using System.Runtime.InteropServices;
using ErrorOr;
using GymManagement.Domain.Rooms;

namespace GymManagement.Domain.Gyms;

public class Gym
{
  private readonly List<Guid> _roomIds = new();
  private readonly int _maxRooms;
  public Guid Id { get; set; }
  public string Name { get; set; }
  public Guid SubscriptionId { get; init; }

  public Gym(string name, Guid subscriptionId, int maxRooms, Guid? id = null)
  {
    Name = name;
    SubscriptionId = subscriptionId;
    _maxRooms = maxRooms;
    Id = id ?? Guid.NewGuid();
  }

  public ErrorOr<Success> AddRoom(Room room)
  {
    // Check if room already is part of this gym
    if (HasRoom(room.Id))
    {
      throw new Exception("Room already exists in gym");
    }

    if (_roomIds.Count() >= _maxRooms)
    {
      return Error.Validation(
        code: "Gyms.CannotHaveMoreRoomsThanTheSubscriptionAllows",
        description: "A gym cannot have more room than the subscription allows"
      );
    }

    _roomIds.Add(room.Id);

    return Result.Success;
  }

  public bool HasRoom(Guid roomId)
  {
    return _roomIds.Contains(roomId);
  }

  public void RemoveRoom(Guid roomId)
  {
    if (!HasRoom(roomId))
    {
      throw new Exception("Room not found");
    }

    _roomIds.Remove(roomId);
  }

  private Gym() { }

}