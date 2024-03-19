namespace GymManagement.Contracts.Gyms;

public record CreateGymRequest(Guid SubscriptionId, string Name);