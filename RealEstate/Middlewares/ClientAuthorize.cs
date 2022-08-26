using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using RealEstate.Controllers;
using RealEstate.Core.Application.Dtos.Account;
using RealEstate.Core.Application.Enums;
using RealEstate.Core.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.RealEstate.Middlewares
{
    public class ClientAuthorize
    {
        private readonly IHttpContextAccessor _httpContextAccesor;

        public ClientAuthorize(IHttpContextAccessor httpContextAccesor)
        {
            _httpContextAccesor = httpContextAccesor;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var user = _httpContextAccesor.HttpContext.Session.Get<AuthenticationResponse>("user");
            if (user != null)
            {
                var isClient = user.Roles.Contains(Roles.Client.ToString());
                if (!isClient)
                {
                    var controller = (HomeController)context.Controller;
                    context.Result = controller.RedirectToAction("AccessDenied", "User");
                }
                else await next();
            }
        }
    }
}
