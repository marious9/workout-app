using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace workout_app.Api.Helpers
{
    public class ValidationExceptionProblemDetails: Microsoft.AspNetCore.Mvc.ProblemDetails
    {
        public ValidationExceptionProblemDetails(ValidationException exception)
        {
            Title = exception.Message;
            Status = StatusCodes.Status400BadRequest;
        }
    }
}
