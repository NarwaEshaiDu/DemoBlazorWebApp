using Blazor2App.Application.Features.Students.Commands;
using Blazor2App.ServiceBus.Infra;
using MassTransit;
using MediatR;

namespace Blazor2App.ConsumerDemo.Features
{
    public class DemoConsumer : BaseConsumer,
        IConsumer<CreateStudentBusCommand>
    {
        private readonly IMediator _mediator;
        public DemoConsumer()
        { }

        public DemoConsumer(IMediator mediator) : base()
        {
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<CreateStudentBusCommand> context)
        {
            try
            {
                Serilog.Log.Logger.Warning("Entering Consume method. MessageId: {MessageId}, CorrelationId: {CorrelationId}", context.MessageId, context.CorrelationId);


                await Consume(context, async () =>
                {
                    var message = context.Message;

                    await _mediator.Send(CreateStudentCommand.CreateCommand("hamza" + GetRandomNumber()), default);
                }, "TriggerStartCreateStudent", "TriggerOkCreateStudent", "TriggerUpdateReadOnlyBugEventFailed", new { context.CorrelationId, context.MessageId, context.Message });

                Serilog.Log.Logger.Warning("Exiting Consume method.");
            }
            catch (Exception ex)
            {
                Serilog.Log.Logger.Error(ex, "An error occurred in the Consume method.");
                throw; // Ensure the exception is propagated for proper error handling.
            }
        }

        private static int GetRandomNumber()
        {
            var rnd = new Random();
            return rnd.Next(0, 11000);
        }
    }
}
