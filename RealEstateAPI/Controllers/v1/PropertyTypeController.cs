using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Core.Application.Dtos.PropertyTypeDtos;
using RealEstate.Core.Application.Features.PropertyTypes.Commands.CreatePropertyType;
using RealEstate.Core.Application.Features.PropertyTypes.Commands.DeletePropertyType;
using RealEstate.Core.Application.Features.PropertyTypes.Commands.UpdatePropertyType;
using RealEstate.Core.Application.Features.PropertyTypes.Queries.GetAllPropertyType;
using RealEstate.Core.Application.Features.PropertyTypes.Queries.GetPropertyTypeById;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Net.Mime;
using System.Threading.Tasks;

namespace RealEstateAPI.WebApi.Controllers.v1
{

    [Authorize(Roles = "Admin, Developer")]
    [SwaggerTag("Property types Manager")]
    [ApiVersion("1.0")]
    public class PropertyTypeController : GeneralController
    {

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PropTypeDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Property type List",
            Description = "Get all Improvements stored in the database"
            )]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await Mediator.Send(new GetAllPropertyTypeQuery()));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PropTypeDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Property Type",
            Description = "Get a property type by its id"
            )]
        public async Task<IActionResult> GetPropById(int id)
        {
            try
            {
                return Ok(await Mediator.Send(new GetPropertyTypeByIdQuery() { Id = id }));
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
            Summary = "new property type",
            Description = "requires some parmas to create a new property type"
            )]
        public async Task<IActionResult> Create([FromBody] CreatePropertyTypeCommand command)
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PropTypeDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "update property type",
            Description = "Update a property type by its id"
            )]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePropertyTypeCommand command)
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
            Summary = "property delete",
            Description = "delete a property type by its id"
            )]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await Mediator.Send(new DeletePropertyTypeByIdCommand { Id = id });
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


    }
}
