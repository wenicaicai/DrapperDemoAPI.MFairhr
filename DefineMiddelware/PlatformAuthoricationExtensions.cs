using Microsoft.AspNetCore.Builder;

namespace DefineMiddelware
{
    public static class PlatformAuthoricationExtensions
    {
        public static IApplicationBuilder UsePlatformAuthorication(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<PlatformAuthoricationMiddleware>();
        }
    }
}
