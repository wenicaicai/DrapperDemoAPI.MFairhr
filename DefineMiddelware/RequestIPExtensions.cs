using DefineMiddelware.Middleware;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace DefineMiddelware
{
    public static class RequestIPExtensions
    {
        public static IApplicationBuilder UseRequestIP(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestIPMiddleware>();
        }
    }
}
