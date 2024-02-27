using System.Text.Json.Serialization;

namespace GymManagement.Contracts.Subscriptions;

[JsonConverter(typeof(JsonStringEnumConverter))] // Allows to serialize the enum
public enum SubscriptionType
{
  Free,
  Starter,
  Pro
}