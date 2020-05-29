using Microsoft.AspNetCore.Builder;

namespace DefineMiddelware
{
    public static class PlatformAuthoricationMiddlewareExtensions
    {
        public static IApplicationBuilder UsePlatformAuthorication(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<PlatformAuthoricationMiddleware>();
        }
    }
}
