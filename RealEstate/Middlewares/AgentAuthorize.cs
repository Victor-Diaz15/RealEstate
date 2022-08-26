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
    public class AgentAuthorize
    {
        private readonly IHttpContextAccessor _httpContextAccesor;

        public AgentAuthorize(IHttpContextAccessor httpContextAccesor)
        {
            _httpContextAccesor = httpContextAccesor;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var user = _httpContextAccesor.HttpContext.Session.Get<AuthenticationResponse>("user");
            if (user != null)
            {
                var isAgent = user.Roles.Contains(Roles.Agent.ToString());
                if (!isAgent)
                {
                    var controller = (HomeController)context.Controller;
                    context.Result = controller.RedirectToAction("AccessDenied", "User");
                }
                else await next();
            }
        }
    }
}
