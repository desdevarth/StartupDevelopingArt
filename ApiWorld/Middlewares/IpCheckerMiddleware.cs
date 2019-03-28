using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace ApiWorld.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class IpCheckerMiddleware
    {
        private readonly RequestDelegate _next;

        public IpCheckerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            if(httpContext.Connection.RemoteIpAddress.IsIPv6SiteLocal)
            return _next(httpContext);

            return httpContext.Response.WriteAsync("Sorry ... Your Ip is Denied");
                
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class IpCheckerMiddlewareExtensions
    {
        public static IApplicationBuilder UseIpCheckerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<IpCheckerMiddleware>();
        }
    }
}
