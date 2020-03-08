using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using workout_app.Core.Domain.Helpers;

namespace workout_app.Api.Helpers
{
    public class NotFoundRuleValidationExceptionProblemDetails : ProblemDetails
    {
        public NotFoundRuleValidationExceptionProblemDetails(NotFoundRuleValidationException exception)
        {
            Title = exception.Message;
            Status = StatusCodes.Status404NotFound;
            Detail = exception.Details;
        }
    }
}
