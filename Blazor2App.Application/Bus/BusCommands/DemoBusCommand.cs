namespace Blazor2App.Application.Bus.BusCommands
{
    public class DemoBusCommand : BaseMessage, IBusCommand
    {
        public DemoBusCommand()
        { }

        public static DemoBusCommand CreateCommand()
        {
            return new DemoBusCommand();
        }
    }
}
