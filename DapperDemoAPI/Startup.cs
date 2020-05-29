using AppDbContext;
using DefineMiddelware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DapperDemoAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //EF 
            var sqlConnection = Configuration.GetConnectionString("DefaultConnection");
            //services.AddDbContext<DbContextModel>(config => config.UseSqlServer(sqlConnection));
            //services.AddDbContext<SysDbContext>();

            services.AddDbContext<SysDbContext>(config => config.UseSqlServer(sqlConnection));

            services.Configure<DbConnection>(Configuration.GetSection("DbConnectionConfig"));

            services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Api 404" });
            });

            services.AddCap(x =>
            {
                x.UseEntityFramework<SysDbContext>();
                x.UseRabbitMQ(option =>
                {
                    option.HostName = "192.168.19.51";
                    option.Port = 5672;
                    option.UserName = "rabbitmq";
                    option.Password = "rabbitmq";
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UsePlatformAuthorication();

            app.UseRouting();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API 404");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //        await context.Response.WriteAsync("Hello World!");
            //    });
            //});endpoints.MapControllerRoute(


        }
    }
}
