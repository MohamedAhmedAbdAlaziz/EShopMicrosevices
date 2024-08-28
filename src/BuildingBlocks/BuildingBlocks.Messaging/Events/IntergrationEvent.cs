using System.Data;

namespace BuildingBlocks.Messaging.Events
{
    public record IntergrationEvent
    {
        public Guid Id { get; set; }
        public DateTime OccurredOn => DateTime.Now;
        public string EventType => GetType().AssemblyQualifiedName!;

    }
}
