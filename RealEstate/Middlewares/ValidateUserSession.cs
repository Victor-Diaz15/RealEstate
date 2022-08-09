﻿using Microsoft.AspNetCore.Http;
using RealEstate.Core.Application.Dtos.Account;
using RealEstate.Core.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.RealEstate.Middlewares
{
    public class ValidateUserSession
    {
        public readonly IHttpContextAccessor _httpContextAccessor;

        public ValidateUserSession(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool HasUser()
        {
            AuthenticationResponse userVm = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            bool validate = userVm == null ? false : true;
            return validate;
        }
    }
}
