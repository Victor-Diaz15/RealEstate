using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Core.Application.Dtos.SalesType;
using RealEstate.Core.Application.Features.Improvements.Queries.GetImprovementById;
using System;
using System.Threading.Tasks;
using RealEstate.Core.Application.Dtos.Improvements;
using RealEstate.Core.Application.Features.Improvements.Queries.GetAllImprovement;
using RealEstate.Core.Application.Features.Improvements.Commands.CreateImprovement;
using RealEstate.Core.Application.Features.Improvements.Commands.UpdateImprovement;
using RealEstate.Core.Application.Features.Improvements.Commands.DeleteImprovements;
using Microsoft.AspNetCore.Authorization;
using System.Net.Mime;
using Swashbuckle.AspNetCore.Annotations;

namespace RealEstateAPI.WebApi.Controllers.v1
{

    [Authorize(Roles = "Admin, Developer")]
    [SwaggerTag("Improvement Manager")]
    [ApiVersion("1.0")]
    public class ImprovementController : GeneralController
    {

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ImprovementDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Improvements List",
            Description = "Get all Improvements stored in the database"
            )]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await Mediator.Send(new GetAllImprovementQuery()));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ImprovementDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Get Improvement",
            Description = "Get an improvement by its id"
            )]
        public async Task<IActionResult> GetSaleById(int id)
        {
            try
            {
                return Ok(await Mediator.Send(new GetImprovementByIdQuery() { Id = id }));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "new Improvememt",
            Description = "Create a new improvement by using all the information  required"
            )]
        public async Task<IActionResult> Create([FromBody] CreateImprovementCommand command)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                await Mediator.Send(command);
                return NoContent();

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ImprovementDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "SaleType update",
            Description = "update a improvement by its id as a param"
            )]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateImprovementCommand command)
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

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "SaleType delete",
            Description = "delete a improvement by its id as a param"
            )]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await Mediator.Send(new DeleteImprovementByIdCommand { Id = id });
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


    }
}
