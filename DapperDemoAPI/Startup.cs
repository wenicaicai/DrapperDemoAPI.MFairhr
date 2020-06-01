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
     * ���Ǹ�����һ�����򿪵���ʹ��ǿ���Google �Ͱٶ������������������ϣ��Լ���Asp.net core 3.1 ��Դ������аݶ���
     * ͬʱ�������ҵ�ʵ���������ж�EndPoint ���˲�һ������ʶ��
     * ˵��������Ӿ���΢���Asp.net core 3.x �Ŀ���йܵ�ģ�͵���ơ�
     *  
     *  ժ���ԣ�https://www.cnblogs.com/jlion/p/12423301.html Author��Jlion
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
                       //ȫ�ֶ����쳣������
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
             * ���ߣ������м������ϵ���...
             * 
             * app.UseRouting();
             * 
             * app.UseEndpoints();//����ͨ·����ʲô���Ĺ�ϵ�� �ս��·�ɹ���ԭ����
             * 
             * app.UseAuthorization();
             * 
             * **/

            //������ 2020.06.01 10:23
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
