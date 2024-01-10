using Blazor2App.Application;
using Blazor2App.Infrastructure.Constants;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Blazor2App.ServiceBus.Infra
{
    public class BusMessageAuthFilter<T> : IFilter<SendContext<T>>, IFilter<ConsumeContext<T>> where T : class
    {
        //TODO: Fix dependency injection issue - right now the filter is called multiple times due to interface inheritence and each time it's resolved we get a new instance.
        public BusMessageAuthFilter(IServiceContext serviceContext, ILogger<BusMessageAuthFilter<T>> logger)
        {
            _serviceContext = serviceContext;
            _logger = logger;
        }

        public async Task Send(SendContext<T> context, IPipe<SendContext<T>> next)
        {
            if (_serviceContext.AuthenticatedUserEmail != null)
                context.Headers.Set(MessageBus.Headers.IdentityEmail, _serviceContext.AuthenticatedUserEmail);

            await next.Send(context);
        }

        public async Task Send(ConsumeContext<T> context, IPipe<ConsumeContext<T>> next)
        {
            var identity = context.Headers.Get<string>(MessageBus.Headers.IdentityEmail, "anonymous@blazor2app.com");
            _serviceContext.AuthenticatedUserEmail = identity;
            _serviceContext.CorrelationId = context.CorrelationId.Value;

            if (_logger.IsEnabled(LogLevel.Debug))
                _logger.LogDebug("Receiving message - Identity header set: {0}", _serviceContext.AuthenticatedUserEmail);

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
