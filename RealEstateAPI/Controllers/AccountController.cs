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
        public async Task<IActionResult> LoginAsync(AuthenticationRequest request)
        {
            return Ok(await _accountSvc.AuthenticationAsync(request));
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterManagerRequest request)
        {
            return Ok(await _accountSvc.RegisterDevUserAsync(request));
        }


        [HttpPost("registerAdmin")]
        public async Task<IActionResult> RegisterAdminAsync(RegisterManagerRequest request)
        {
            return Ok(await _accountSvc.RegisterAdminUserAsync(request));
        }



    }
}
