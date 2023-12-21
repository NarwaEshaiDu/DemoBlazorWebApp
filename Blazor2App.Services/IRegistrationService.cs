namespace Blazor2App.Services
{
    public interface IRegistrationService
    {
        Task<Registration> SubmitRegistration(string eventId, string memberId, decimal payment);
    }
}