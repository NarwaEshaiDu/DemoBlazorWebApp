namespace Blazor2App.Application.Bus
{
    public interface IBusPublisher
    {
        /// <summary>
        /// Send a command to a given queue.
        /// </summary>
        /// <param name="command"></param>
        /// <param cancellationToken=""><see cref="CancellationToken"/></param>
        /// <returns></returns>
        Task SendAsync<T>(T command, CancellationToken cancellationToken) where T : BaseMessage, IBusCommand;
    }
}
