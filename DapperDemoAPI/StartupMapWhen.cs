using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperDemoAPI
{
    public class StartupMapWhen
    {
        const string queryKeyName = "wasteYear";

        public void Configure(IApplicationBuilder app)
        {
            app.MapWhen(context => context.Request.Query.ContainsKey(queryKeyName), HandleBranch);

            app.Run(async context =>
            {
                await context.Response.WriteAsync("<p> Hello from non-Map delegate. </p>");
            });
        }

        public static void HandleBranch(IApplicationBuilder app)
        {
            app.Run(async (context) =>
            {
                var branchVal = context.Request.Query[queryKeyName];
                await context.Response.WriteAsync($" Mile has already waste {branchVal} years and now he want to keep learning to imporve himself and get something he lose in the pass.");
            });
        }
    }
}
