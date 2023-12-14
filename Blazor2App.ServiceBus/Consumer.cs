using MassTransit;
using Microsoft.Extensions.Logging;

namespace Blazor2App.ServiceBus
{
    public class Consumer : IConsumer<Hello>
    {
        private readonly ILogger<Consumer> _logger;
        public Consumer(ILogger<Consumer> logger)
        {
            _logger = logger;
        }
        public Task Consume(ConsumeContext<Hello> context)
        {
            _logger.LogInformation("idk what im doing {name}", context.Message.Name);
            return Task.CompletedTask;
        }
    }
}
