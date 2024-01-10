using Blazor2App.Application;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Blazor2App.ServiceBus.Infra
{
    public class BusMessageCorrelationFilter<T> : IFilter<SendContext<T>>, IFilter<ConsumeContext<T>> where T : class
    {
        //TODO: Fix dependency injection issue - right now the filter is called multiple times due to interface inheritence and each time it's resolved we get a new instance.
        public BusMessageCorrelationFilter(IServiceContext serviceContext, ILogger<BusMessageCorrelationFilter<T>> logger)
        {
            _serviceContext = serviceContext;
            _logger = logger;
        }

        public async Task Send(SendContext<T> context, IPipe<SendContext<T>> next)
        {
            context.CorrelationId = _serviceContext.CorrelationId;

            await next.Send(context);

            if (_logger.IsEnabled(LogLevel.Debug))
                _logger.LogDebug($"Message sent.", new { context.CorrelationId, context.ConversationId });
        }

        public async Task Send(ConsumeContext<T> context, IPipe<ConsumeContext<T>> next)
        {
            _serviceContext.CorrelationId = context.CorrelationId.Value;

            if (_logger.IsEnabled(LogLevel.Debug))
                _logger.LogDebug($"Message received.", new { context.CorrelationId, context.ConversationId });

            await next.Send(context);
        }

        public void Probe(ProbeContext context)
        {
            context.CreateFilterScope("correlationFilter");
        }

        private readonly IServiceContext _serviceContext;
        private readonly ILogger _logger;
    }
}
