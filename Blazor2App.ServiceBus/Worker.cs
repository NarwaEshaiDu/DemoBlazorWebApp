using Blazor2App.Application.Bus;
using Blazor2App.Application.Features.Students.Commands;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Blazor2App.ServiceBus
{
    public class Worker : BackgroundService, IBusPublisher
    {
        private readonly IBusPublisher _busPublisher;
        public Worker(IBusPublisher busPublisher)
        {
            _busPublisher = busPublisher;
        }

        public async Task SendAsync<T>(T command, CancellationToken cancellationToken) where T : BaseMessage, IBusCommand
        {
            await _busPublisher.SendAsync(command, cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await this.SendAsync(new CreateStudentBusCommand(), cancellationToken);

                Log.Logger.Error("hi, im the worker and i publish every second a new post.");
                
                await Task.Delay(1000, cancellationToken);
            }
        }
    }
}
