namespace Blazor2App.Application.Bus
{
    public interface IBusCommand
    {
        public Guid CorrelationId { get; set; }
    }
}
