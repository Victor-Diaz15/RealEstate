using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Core.Application.Dtos.UserAccounts;
using RealEstate.Core.Application.Features.Agents.Queries.GetAgentById;
using RealEstate.Core.Application.Features.Agents.Queries.GetAllAgent;
using RealEstate.Core.Application.Features.ChangeStatus.Commands.ChangeStatus;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Net.Mime;
using System.Threading.Tasks;

namespace RealEstateAPI.WebApi.Controllers.v1
{
    [Authorize(Roles = "Admin, Developer")]
    [SwaggerTag("Agent Manager")]
    [ApiVersion("1.0")]
    public class AgentController : GeneralController
    {

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AgentDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "List of agents",
            Description = "Get all that are registered on this application"
            )]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await Mediator.Send(new GetAllAgentQuery()));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AgentDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Agent",
            Description = "Get an agent by its id"
            )]
        public async Task<IActionResult> GetAgentById(string id)
        {
            try
            {
                return Ok(await Mediator.Send(new GetAgentByIdQuery() { Id = id }));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetAgentProperty/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AgentDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Agent's properties",
            Description = "Get all the properties related to an agent"
            )]
        public async Task<IActionResult> GetAgentProperty(string id)
        {
            try
            {
                return Ok(await Mediator.Send(new GetAgentPropertyQuery() { id = id }));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [Authorize(Roles = "Admin")]
        [HttpPut("ChangeStatus/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AgentDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "Status",
            Description = "Change the status of an agent. It can be true or false"
            )]
        public async Task<IActionResult> ChangeStatus(string id, [FromBody] ChangeAgentStatusCommand command)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                if (id != command.Id)
                {
                    return BadRequest();
                }

                return Ok(await Mediator.Send(command));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
