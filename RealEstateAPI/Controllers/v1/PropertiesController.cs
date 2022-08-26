using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Core.Application.Dtos.Properties;
using RealEstate.Core.Application.Features.Propertys.Queries.GetAllProperty;
using RealEstate.Core.Application.Features.Propertys.Queries.GetPropertyById;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Net.Mime;
using System.Threading.Tasks;

namespace RealEstateAPI.WebApi.Controllers.v1
{
    [Authorize(Roles = "Admin, Developer")]
    [ApiVersion("1.0")]
    [SwaggerTag("Properties Manager")]
    public class PropertiesController : GeneralController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "List of properties",
            Description = "Get all properties stored on the database"
            )]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await Mediator.Send(new GetAllPropertyQuery()));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PropertyDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Property",
            Description = "Get a property by its id"
            )]
        public async Task<IActionResult> GetPropertyById(int id)
        {
            try
            {

                return Ok(await Mediator.Send(new GetPropertyByIdQuery() { Id = id }));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetByCode/{code}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PropertyDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Property",
            Description = "Get a property by its code"
            )]
        public async Task<IActionResult> GetPropertyByCode(int code)
        {
            var codeString = code.ToString();
            try
            {
                return Ok(await Mediator.Send(new GetPropertyByCodeQuery() { Code = codeString }));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }





    }
}
