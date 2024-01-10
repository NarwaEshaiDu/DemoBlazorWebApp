using Blazor2App.Application;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Blazor2App.ServiceBus.Infra
{
    public class BusMessageExceptionFilter<T> : IFilter<SendContext<T>>, IFilter<ConsumeContext<T>> where T : class
    {
        public BusMessageExceptionFilter(ILogger<BusMessageCorrelationFilter<T>> logger)
        {
            _logger = logger;
        }

        public async Task Send(SendContext<T> context, IPipe<SendContext<T>> next)
        {
            try
            {
                await next.Send(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending a message.", new { Data = context.Message, context.CorrelationId, context.ConversationId });
            }
        }

        public async Task Send(ConsumeContext<T> context, IPipe<ConsumeContext<T>> next)
        {
            _logger.LogDebug($"Message received.", new { Data = context.Message, context.CorrelationId, context.ConversationId });

            try
            {
                await next.Send(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing message.", new { Data = context.Message, context.CorrelationId, context.ConversationId });
            }
        }

        public void Probe(ProbeContext context)
        {
            context.CreateFilterScope("correlationFilter");
        }

        private readonly ILogger _logger;
    }
}
