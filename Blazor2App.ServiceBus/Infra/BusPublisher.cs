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
            //retry policy ? 
            if (command.CorrelationId == Guid.Empty || command.CorrelationId == default)
            {
                command.CorrelationId = Guid.NewGuid();
            }

            await _bus.Publish(command, cancellationToken);
        }
    }
}