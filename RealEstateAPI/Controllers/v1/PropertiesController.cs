using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Core.Application.Features.Properties.Queries.GetAllProperties;
using RealEstate.Core.Application.Features.Properties.Queries.GetPropertyById;
using System;
using System.Threading.Tasks;

namespace RealEstateAPI.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class PropertiesController : GeneralController
    {

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(GetAllPropertyParameter param )
        {
            try
            {
                return Ok(await Mediator.Send( new GetAllPropertyQuery() { Id = param.Id}));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        //[HttpGet]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //public async Task<IActionResult> GetPropById(int id)
        //{
        //    try
        //    {
        //        return Ok(await Mediator.Send(new GetPropertyByIdQuery() { Id = id }));
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}

        //[HttpGet]
        //public async Task<IActionResult> GetByCode(string code)
        //{
        //    try
        //    {
        //        return Ok(await Mediator.Send(new GetPropertyByCodeQuery() { Code = code }));
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}

       



    }
}
