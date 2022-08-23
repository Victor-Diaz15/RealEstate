using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Core.Application.Dtos.Properties;
using RealEstate.Core.Application.Features.Propertys.Queries.GetAllProperty;
using RealEstate.Core.Application.Features.Propertys.Queries.GetPropertyById;
using System;
using System.Threading.Tasks;

namespace RealEstateAPI.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class PropertiesController : GeneralController
    {

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await Mediator.Send( new GetAllPropertyQuery()));
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

        [HttpGet("{code}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PropertyDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPropertyByCode([FromBody]string code)
        {
            try
            {
                return Ok(await Mediator.Send(new GetPropertyByCodeQuery() { Code = code }));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }





    }
}
