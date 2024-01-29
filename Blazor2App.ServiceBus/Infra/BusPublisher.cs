using Blazor2App.Application.Bus;
using MassTransit;

namespace Blazor2App.ServiceBus.Infra
{
    public class BusPublisher : IBusPublisher
    {
        private readonly IBus _bus;

        public BusPublisher(IBus bus)
        {
            _bus = bus;
        }

        public async Task SendAsync<T>(T command, CancellationToken cancellationToken) where T : BaseMessage, IBusCommand
        {
            //TODO: check if there is already a correlation id, if not make one else use the existing one
            Guid correlationId = Guid.NewGuid();
            command.CorrelationId = correlationId;

            await _bus.Publish(command, cancellationToken);
        }
    }
}