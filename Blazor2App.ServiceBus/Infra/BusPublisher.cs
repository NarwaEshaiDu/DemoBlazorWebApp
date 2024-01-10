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
            await _bus.Send(command, cancellationToken);
        }
    }
}