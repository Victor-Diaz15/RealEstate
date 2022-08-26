using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Core.Application.Dtos.Account;
using RealEstate.Core.Application.Enums;
using RealEstate.Core.Application.Helpers;
using RealEstate.Core.Application.Interfaces.Services;
using System.Threading.Tasks;

namespace RealEstateAPI.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Authorize(Roles = "Admin, Developer")]
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
        public async Task<IActionResult> LoginAsync(AuthenticationRequest request)
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
        public async Task<IActionResult> RegisterAsync(RegisterManagerDevRequest request)
        {
            return Ok(await _accountSvc.RegisterDevUserAsync(request));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("registerAdmin")]
        public async Task<IActionResult> RegisterAdminAsync(RegisterManagerDevRequest request)
        {
            var methodRequest = _mapper.Map<RegisterManagerRequest>(request);
            return Ok(await _accountSvc.RegisterAdminUserAsync(methodRequest));
        }



    }
}
