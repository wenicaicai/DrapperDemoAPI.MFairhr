using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperDemoAPI
{
    public class StartupMap
    {
        public void Configure(IApplicationBuilder app)
        {
            app.Map("/MapI", HandleMapI);
            app.Map("/MapII", HandleMapII);
            app.Run(async context =>
            {
                await context.Response.WriteAsync("Handle when no Map...\r\n");
            });
        }

        public static void HandleMapI(IApplicationBuilder app)
        {
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Handle when Map I\r\n");
            });
        }

        public static void HandleMapII(IApplicationBuilder app)
        {
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Handle when Map II\r\n");
            });
        }
    }
}
