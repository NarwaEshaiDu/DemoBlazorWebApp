﻿namespace Blazor2App.Shared.Contracts
{
    public class RegistrationSubmitted
    {
        public Guid RegistrationId { get; init; }
        public DateTime RegistrationDate { get; init; }
        public string MemberId { get; init; } = null!;
        public string EventId { get; init; } = null!;
        public decimal Payment { get; init; }
    }
}
