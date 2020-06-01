using AppDbContext;
using DapperDemoAPI.Filters;
using DefineMiddelware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DapperDemoAPI
{

    /**
     * 
     * 还是跟往常一样，打开电脑使用强大的Google 和百度搜索引擎查阅相关资料，以及打开Asp.net core 3.1 的源代码进行拜读，
     * 同时终于在我的实践及测试中对EndPoint 有了不一样的认识，
     * 说到这里更加敬佩微软对Asp.net core 3.x 的框架中管道模型的设计。
     *  
     *  摘抄自：https://www.cnblogs.com/jlion/p/12423301.html Author：Jlion
     * **/
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

            services.AddMvc(options =>
                   {
                       options.EnableEndpointRouting = false;
                       //全局定义异常过滤器
                       options.Filters.Add<CustomerExceptionFilterAttribute>();
                       //options.Filters.Add(typeof(CustomerExceptionFilterAttribute));
                   })
                .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0);

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

            app.UseStaticFiles();

            app.UseMvcWithDefaultRoute();

            //app.UseMvc();

            /**
             * 
             * 三者（三个中间件）关系如何...
             * 
             * app.UseRouting();
             * 
             * app.UseEndpoints();//跟普通路由有什么样的关系？ 终结点路由工作原理解读
             * 
             * app.UseAuthorization();
             * 
             * **/

            //不可行 2020.06.01 10:23
            //app.UseRouting();
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API 404");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });

        }
    }
}
