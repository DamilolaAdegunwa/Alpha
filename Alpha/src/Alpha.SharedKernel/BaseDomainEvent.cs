using MediatR;

namespace Alpha.SharedKernel
{
  public abstract class BaseDomainEvent : INotification
  {
    /// <summary>
    /// time the event occured (generic to all events)
    /// </summary>
    public DateTime DateOccurred { get; protected set; } = DateTime.UtcNow;
  }
}
