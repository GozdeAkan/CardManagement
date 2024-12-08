using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Utils
{
    public static class WorkContext
    {
        private static IHttpContextAccessor _httpContextAccessor = null!;

        public static void SetHttpContextAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public static string? CurrentEmailAddress
        {
            get
            {
                HttpContext httpContext = _httpContextAccessor.HttpContext;
                if (httpContext != null)
                {
                    ClaimsPrincipal claimsPrincipal = httpContext.User;
                    if (claimsPrincipal != null)
                    {
                        String? claimUserId = claimsPrincipal.FindFirstValue("emailaddress");
                        return claimUserId;
                    }
                }

                return null;
            }
        }
    }
}
