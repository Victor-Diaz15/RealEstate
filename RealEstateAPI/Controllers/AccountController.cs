using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Core.Application.Dtos.Account;
using RealEstate.Core.Application.Interfaces.Services;
using System.Threading.Tasks;

namespace RealEstateAPI.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountSvc;

        public AccountController(IAccountService accountSvc)
        {
            _accountSvc = accountSvc;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateAsync(AuthenticationRequest request)
        {
            return Ok(await _accountSvc.AuthenticationAsync(request));
        }
        //[HttpPost("register")]
        //public async Task<IActionResult> RegisterAsync(AuthenticationRequest request)
        //{
        //    var origin = Request.Headers["origin"];
        //    //RegisterBasicRequest basicUser = 
        //    //var user = _mapper.Map
        //    return Ok(await _accountSvc.RegisterBasicUserAsync(request, origin));
        //}


    }
}
