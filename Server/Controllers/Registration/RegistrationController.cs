using Blazor2App.Services;
using Blazor2App.Shared.Models.Registration;
using Microsoft.AspNetCore.Mvc;

namespace Blazor2App.Server.Controllers.Registration
{
    public class RegistrationController : ControllerBase
    {
        private readonly IRegistrationService _registrationService;
       public RegistrationController(IRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] RegistrationModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var registration = await _registrationService.SubmitRegistration(model.EventId, model.MemberId, model.Payment);

                return Ok(new
                {
                    registration.RegistrationId,
                    registration.RegistrationDate,
                    registration.MemberId,
                    registration.EventId,
                });
            }
            catch (Exception e)
            {
                return Conflict(new
                {
                    model.MemberId,
                    model.EventId,
                });
            }
        }
    }
}
