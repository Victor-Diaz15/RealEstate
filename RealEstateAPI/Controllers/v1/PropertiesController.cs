using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Core.Application.Features.Properties.Queries.GetAllProperties;
using System;
using System.Threading.Tasks;

namespace RealEstateAPI.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class PropertiesController : GeneralController
    {

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllProperties(GetAllPropertyParameter param )
        {
            try
            {
                return Ok(await mediator.Send( new GetAllPropertyQuery() { Id = param.Id}));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
