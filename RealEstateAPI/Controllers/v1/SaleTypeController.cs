using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Core.Application.Dtos.SalesType;
using RealEstate.Core.Application.Features.SalesType.Commands.CreateSaleType;
using RealEstate.Core.Application.Features.SalesType.Commands.DeleteSalesType;
using RealEstate.Core.Application.Features.SalesType.Queries.GetAllSaleType;
using RealEstate.Core.Application.Features.SalesType.Commands.UpdateSaleType;
using RealEstate.Core.Application.Features.SaleTypes.Queries.GetSaleTypeById;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Net.Mime;
using Swashbuckle.AspNetCore.Annotations;

namespace RealEstateAPI.WebApi.Controllers.v1
{

    [Authorize(Roles = "Admin, Developer")]
    [SwaggerTag("Sale type Manager")]
    [ApiVersion("1.0")]
    public class SaleTypeController : GeneralController
    {

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SaleTypeDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Sale Type List",
            Description = "Get all sale types that are stored in the database"
            )]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await Mediator.Send(new GetAllSaleTypeQuery()));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SaleTypeDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Sale Type",
            Description = "Get a sale type by its id"
            )]
        public async Task<IActionResult> GetSaleById(int id)
        {
            try
            {
                return Ok(await Mediator.Send(new GetSaleTypeByIdQuery() { Id = id }));
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
            Summary = "new sale type",
            Description = "requires some parmas to create a new sale type"
            )]
        public async Task<IActionResult> Create([FromBody] CreateSaleTypeCommand command)
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SaleTypeDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "update sale type",
            Description = "Update a sale type by its id"
            )]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateSaleTypeCommand command)
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
            Description = "delete a sale type by its id"
            )]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await Mediator.Send(new DeleteSaleTypeByIdCommand { Id = id });
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


    }
}
