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
        private readonly WorkoutAppDbContext _dbContext;

        public ExercisesController(IMediator mediator, WorkoutAppDbContext dbContext)
        {
            _mediator = mediator;
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllExercises()
        {
            var query = new GetAllExercisesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateExercise([FromBody] CreateExerciseCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction("CreateExercise", new { id = result.Id });

        }

    }
}
