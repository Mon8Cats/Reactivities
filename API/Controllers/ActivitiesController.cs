using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Activities;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivitiesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ActivitiesController(IMediator mediator)
        {
            _mediator = mediator;

        }


        [HttpGet]
        public async Task<ActionResult<List<Activity>>> List()
        //public async Task<ActionResult<List<Activity>>> List(CancellationToken cancellationToken)
        {
            return await _mediator.Send(new List.ListQuery());
            //return await _mediator.Send(new List.Query(), cancellationToken);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Activity>> Details(Guid id)
        {
            return await _mediator.Send(new Details.DetailsQuery { Id = id });
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create(Create.CreateCommand command) // no need [FromBody]
        {
            //if (!ModelState.IsValid)
            //    return BadRequest(ModelState);
            return await _mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Edit(Guid id, Edit.EditCommand command)
        {
            command.Id = id;
            return await _mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(Guid id)
        {
            return await _mediator.Send(new Delete.DeleteCommand { Id = id });
        }
    }

}