using System.Runtime.Serialization;

namespace Blazor2App.Application.Infra.Exceptions
{
    public class OptimisticConcurrencyException : Exception
    {
        public OptimisticConcurrencyException() { }

        public OptimisticConcurrencyException(string message) : base(message) { }
    }
}
