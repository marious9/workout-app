using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using workout_app.Application.Commands;
using workout_app.Application.Queries;
using workout_app.Infrastructure.Configuration;

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

        [HttpGet]
        public async Task<IActionResult> GetAllExercises()
        {
            var query = new GetAllExercises.GetAllExercisesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{exerciseId}")]
        public async Task<IActionResult> GetExerciseById(int exerciseId)
        {
            var query = new GetExerciseById.GetExerciseByIdQuery(exerciseId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateExercise([FromBody] CreateExercise.CreateExerciseCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction("CreateExercise", new { id = result });
        }

    }
}
