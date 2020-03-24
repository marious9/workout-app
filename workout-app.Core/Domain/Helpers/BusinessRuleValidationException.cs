using System;

namespace workout_app.Core.Domain.Helpers
{
    public class BusinessRuleValidationException : Exception
    {
        public string Details { get; }

        public BusinessRuleValidationException(string message) : base(message)
        {

        }

        public BusinessRuleValidationException(string message, string details) : base(message)
        {
            Details = details;
        }
    }
}
