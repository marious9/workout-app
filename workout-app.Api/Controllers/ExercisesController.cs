using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using workout_app.Application.Queries.GetAllExercises;

namespace workout_app.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExercisesController : ControllerBase
    {      
        private readonly IMediator _mediator;

        public ExercisesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> GetAllExercises()
        {
            var query = new GetAllExercisesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

    }
}
