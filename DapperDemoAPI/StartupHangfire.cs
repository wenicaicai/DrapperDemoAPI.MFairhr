using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DapperDemoAPI
{
    public class StartupHangfire
    {
        private IConfiguration _configuration;

        public StartupHangfire(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            var sqlConnection = _configuration.GetConnectionString("DefaultConnection");
            services.AddHangfire(x => x.UseSqlServerStorage(sqlConnection));
            services.AddHangfireServer();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseHangfireDashboard();
        }
    }
}
