using System;

namespace workout_app.Core.Domain.Helpers
{
    public class NotFoundRuleValidationException : Exception
    {
        public string Details { get; }

        public NotFoundRuleValidationException(string message) : base(message)
        {

        }

        public NotFoundRuleValidationException(string message, string details) : base(message)
        {
            Details = details;
        }
    }
}
