using MassTransit;
using Newtonsoft.Json;
using Serilog;

namespace Blazor2App.ServiceBus.Infra
{
    public abstract class BaseConsumer
    {
        public BaseConsumer()
        { }
        // Application insight will use the start, ok and error - events.
        // custom made events to filter on easily.
        public virtual async Task Consume<T>(ConsumeContext<T> context, Func<Task> func, string startEvent, string okEvent, string errorEvent, object eventProperties) where T : class
        {
            await func();

            Log.Logger.Information("The correlactionId is :'{0}'", context.CorrelationId!.Value);
            Log.Logger.Information("Done processing message of type '{0}'. Message content: {1}", typeof(T), JsonConvert.SerializeObject(context.Message));
            await Task.CompletedTask;
        }
    }
}
