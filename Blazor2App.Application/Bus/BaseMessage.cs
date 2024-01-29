namespace Blazor2App.Application.Bus
{
    public abstract class BaseMessage : IBusCommand
    {
        public Guid CorrelationId { get; set; }
        public string AuthenticatedUserEmail { get; set; }
        public Guid? MessageId { get; set; }
    }
}
