using Blazor2App.Application;
using Blazor2App.Infrastructure.Constants;
using MassTransit;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Blazor2App.ServiceBus.Infra
{
    public abstract class BaseConsumer
    {
        private readonly ILogger<BaseConsumer> _logger;

        public BaseConsumer()
        { }

        public BaseConsumer(ILogger<BaseConsumer> logger)
        {
            _logger = logger;
        }

        public virtual async Task Consume<T>(ConsumeContext<T> context, Func<Task> func, string startEvent, string okEvent, string errorEvent, object eventProperties) where T : class
        {
            await func();

           //var test  = context.CorrelationId.Value;
            //_context.MessageId = context.MessageId!.Value;
            //_context.AuthenticatedUserEmail = context.Headers.Get(MessageBus.Headers.IdentityEmail, "anonymous@blazor2app.com")!;

            //_logger.Log(LogLevel.Information, "Done processing message of type '{0}'. Message content: {1}", typeof(T), JsonConvert.SerializeObject(context.Message));
            await Task.CompletedTask;
        }
    }
}
