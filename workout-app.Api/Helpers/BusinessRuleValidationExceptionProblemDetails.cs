using Microsoft.AspNetCore.Http;
using workout_app.Core.Domain.Helpers;

namespace workout_app.Api.Helpers
{
    public class BusinessRuleValidationExceptionProblemDetails : Microsoft.AspNetCore.Mvc.ProblemDetails
    {
        public BusinessRuleValidationExceptionProblemDetails(BusinessRuleValidationException exception)
        {
            Title = exception.Message;
            Status = StatusCodes.Status409Conflict;
            Detail = exception.Details;            
        }
    }
}
