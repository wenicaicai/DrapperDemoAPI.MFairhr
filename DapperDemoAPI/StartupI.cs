using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperDemoAPI
{
    public class StartupI
    {
        public void Configure(IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync($"Hi,{DateTime.UtcNow},Begin handle the request...\r\n");
                //调用下一个Middleware
                //当不调用{next}时,也会发生短路.
                await next.Invoke();
                await context.Response.WriteAsync($"Hi,{DateTime.UtcNow},Finish handle the request...\r\n");

            });
            //调用Run会触发短路，不会继续请求下一个{next}
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync($"Running the Middleware... when {DateTime.UtcNow}\r\n");
            });
        }
    }
}
