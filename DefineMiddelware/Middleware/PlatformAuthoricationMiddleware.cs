using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DefineMiddelware
{
    public class PlatformAuthoricationMiddleware
    {
        private RequestDelegate _next;
        private readonly ILogger _logger;
        private static int countRequestCount { get; set; }

        public PlatformAuthoricationMiddleware(RequestDelegate next, ILoggerFactory logger)
        {
            _next = next;
            _logger = logger.CreateLogger<PlatformAuthoricationMiddleware>(); ;
        }

        public async Task Invoke(HttpContext context)
        {
            countRequestCount++;
            _logger.LogInformation("Beging to run middleware...");
            await _next.Invoke(context);
            _logger.LogInformation($"Finish to run middleware...requestCount:{countRequestCount}");
        }
    }
}
