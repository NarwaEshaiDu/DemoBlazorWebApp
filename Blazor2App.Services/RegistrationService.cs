using Blazor2App.Database.Base;
using Blazor2App.Shared.Contracts;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Blazor2App.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly RegistrationDbContext _dbContext;
        private readonly IPublishEndpoint _publishEndpoint;

        public RegistrationService(RegistrationDbContext dbContext, IPublishEndpoint publishEndpoint)
        {
            _dbContext = dbContext;
            _publishEndpoint = publishEndpoint;
        }
        public async Task<Registration> SubmitRegistration(string eventId, string memberId, decimal payment)
        {
            var registration = new Registration
            {
                RegistrationId = NewId.NextGuid(),
                RegistrationDate = DateTime.UtcNow,
                MemberId = memberId,
                EventId = eventId,
                Payment = payment
            };

            await _dbContext.Set<Registration>().AddAsync(registration);

            await _publishEndpoint.Publish(new RegistrationSubmitted
            {
                RegistrationId = registration.RegistrationId,
                RegistrationDate = registration.RegistrationDate,
                MemberId = registration.MemberId,
                EventId = registration.EventId,
                Payment = payment
            });

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException exception)
            {
                //TODO
            }

            return registration;
        }
    }
}
