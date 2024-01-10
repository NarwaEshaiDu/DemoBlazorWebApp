namespace Blazor2App.Application.Bus
{
    public abstract class BaseMessage
    {
        public Guid Id { get; set; }
    }
}
