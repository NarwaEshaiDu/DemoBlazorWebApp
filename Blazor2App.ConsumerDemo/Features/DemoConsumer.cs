using Blazor2App.Application;
using Blazor2App.Application.Bus.BusCommands;
using Blazor2App.Application.Features.Students.Commands;
using Blazor2App.Application.Features.Students.Queries;
using Blazor2App.ServiceBus.Infra;
using MassTransit;
using MediatR;

namespace Blazor2App.ConsumerDemo.Features
{
    public class DemoConsumer : BaseConsumer, IConsumer<DemoBusCommand>
    {
        private readonly IMediator _mediator;
        public DemoConsumer()
        { }

        public DemoConsumer(Microsoft.Extensions.Logging.ILogger<DemoConsumer> logger, IServiceContext serviceContext, IMediator mediator) : base(logger, serviceContext) 
        { 
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<DemoBusCommand> context)
        {
            await Consume(context, async () =>
            {
                var message = context.Message;
                
                await _mediator.Send(CreateStudentCommand.CreateCommand("hamza"), default);
                
            }, "TriggerStartCreateStudent", "TriggerOkCreateStudent", "TriggerUpdateReadOnlyBugEventFailed", new { context.CorrelationId, context.MessageId, context.Message });
        }
    }
}
