using MediatR;
using Serilog;
using Serilog.Context;
using Serilog.Events;

namespace Blazor2App.Server.Infra.Mediator
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            using (LogContext.PushProperty("request", request, true))
            {
                using (Log.Logger.BeginTimedOperation("Mediator handler", null, LogEventLevel.Debug, TimeSpan.FromMilliseconds(200), propertyValues: typeof(TRequest).Name))
                {
                    Log.Error("Start {requestType}", typeof(TRequest).Name);
                    var response = await next();
                    Log.Error("Finished {requestType}", typeof(TRequest).Name);
                    return response;
                }
            }
        }
    }
}
