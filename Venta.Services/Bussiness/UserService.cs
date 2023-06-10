using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Venta.Services.Interface;

namespace Venta.Services.Bussiness
{
    public class UserService : IUserService
    {
        private readonly HttpContext _httpContext;

        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContext = httpContextAccessor.HttpContext;
        }

        public string GetUserName()
        {
            if (_httpContext.User.Identity != null && _httpContext.User.Identity.IsAuthenticated)
            {
                var claim = _httpContext.User.Claims.Where(x => x.Type == ClaimTypes.Name).FirstOrDefault();                
                return claim.Value.ToString();
            }
            else
            {
                throw new ApplicationException("El usuario no esta authenticado");
            }
        }
    }
}
