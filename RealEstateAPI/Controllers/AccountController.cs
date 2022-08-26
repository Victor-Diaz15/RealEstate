using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Core.Application.Dtos.Account;
using RealEstate.Core.Application.Enums;
using RealEstate.Core.Application.Helpers;
using RealEstate.Core.Application.Interfaces.Services;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;
using System.Threading.Tasks;

namespace RealEstateAPI.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Authorize(Roles = "Admin, Developer")]
    [SwaggerTag("Account login and registration")]

    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountSvc;
        private readonly IMapper _mapper;

        public AccountController(IAccountService accountSvc, IMapper mapper)
        {
            _accountSvc = accountSvc;
            _mapper = mapper;
        }

        [HttpPost("authenticate")]
        [AllowAnonymous]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "log in",
            Description = "allows admin and developers user to log in and use the methods they are allowed to use"
            )]
        public async Task<IActionResult> LoginAsync([FromBody] AuthenticationRequest request)
        {
            var user = await _accountSvc.AuthenticationAsync(request);
            int admin = (int)Roles.Admin;
            int developer = (int)Roles.Developer;

            if (request.Email == null || request.Password == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Email and password fields required");
            }

            if (user.Id == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "User does not exists");

            }
            
            if (user.TypeUser != admin && user.TypeUser != developer)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Just Admin and Devs are allowed to log in");
            }

            return Ok(user);
            
        }

        [Authorize(Roles = "Admin, Developer")]
        [HttpPost("register")]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "Devs Registration",
            Description = "To create a new Developer Account/User"
            )]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterManagerDevRequest request)
        {
            return Ok(await _accountSvc.RegisterDevUserAsync(request));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("registerAdmin")]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "Admin Registration",
            Description = "To create a new Admin Account/User"
            )]
        public async Task<IActionResult> RegisterAdminAsync([FromBody] RegisterManagerDevRequest request)
        {
            var methodRequest = _mapper.Map<RegisterManagerRequest>(request);
            return Ok(await _accountSvc.RegisterAdminUserAsync(methodRequest));
        }



    }
}
