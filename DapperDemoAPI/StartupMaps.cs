using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperDemoAPI
{
    public class StartupMaps
    {
        const string programmer = "programmer";
        const string programmerLevelI = "Primary";
        const string programmerLevelII = "Middle";
        const string programmerLevelIII = "Senior";

        public void Configure(IApplicationBuilder app)
        {
            app.Map($"/{programmer}", programmerLevel =>
            {
                programmerLevel.Map($"/{programmerLevelI}", PrimaryProgrammer);
                programmerLevel.Map($"/{programmerLevelII}", MiddleProgrammer);
                programmerLevel.Map($"/{programmerLevelIII}", SeniorProgrammer);
            });

            app.Run(async context => await context.Response.WriteAsync("Other job to make money for life..."));
        }

        public void PrimaryProgrammer(IApplicationBuilder app)
        {
            app.Run(async context => await context.Response.WriteAsync("When you are a primary programmer,keep learning,thinking and sleeping and keep sincerely."));
        }

        public void MiddleProgrammer(IApplicationBuilder app)
        {
            app.Run(async context => await context.Response.WriteAsync("When you are a middle programmer,you can solve much more problem in the daliy working,and feel program are running well in your mind."));
        }

        public void SeniorProgrammer(IApplicationBuilder app)
        {
            app.Run(async context => await context.Response.WriteAsync("When you are reach to senior level,you will solve more problem and create huge value for others.")); ;
        }
    }
}
