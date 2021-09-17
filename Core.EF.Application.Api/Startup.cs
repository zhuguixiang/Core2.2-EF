using Core.EF.Application.Commom;
using Core.EF.Application.Core;
using Core.EF.Application.Models;
using Core.EF.Application.Web.Commom;
using log4net;
using log4net.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;

namespace Core.EF.Application.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            AutoMapperConfig.Initialize();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //允许跨域调用
            services.AddCors();

            //log4net
            //必须先注册，如果被其他服务提前调用会报错
            var repository = LogManager.CreateRepository("NETCoreLogRepository");
            XmlConfigurator.Configure(repository, new FileInfo("log4net.config"));
            LogService.Instance.SetRepository(repository);

            //appSettings
            services.AddOptions();
            IConfigurationSection configurationSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(configurationSection);

            //redis
            string redisConnectString = configurationSection.GetSection("RedisCache").GetValue(typeof(string), "ConnectionString").ToString();
            CachingService.Instance.Connect(redisConnectString);
            services.AddSingleton<CachingService>(CachingService.Instance);

            //注册Core 2.2
            services
                .AddMvc(options =>
                {
                    options.Filters.Add<HttpAuthenticationFilter>();
                    options.Filters.Add<HttpGlobalExceptionFilter>();
                })
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //注册服务连接数据库
            EFCoreContex.ConnectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContextPool<EFCoreContex>(options => options.UseOracle(EFCoreContex.ConnectionString));

            //注册Swagger
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info
                {

                    Version = "v1",
                    Title = "API 文档",
                    Description = "Swagger文档"
                });

                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "Core.EF.Application.Api.xml");
                options.IncludeXmlComments(xmlPath);

                xmlPath = Path.Combine(basePath, "Core.EF.Application.Models.xml");
                options.IncludeXmlComments(xmlPath);

                xmlPath = Path.Combine(basePath, "Core.EF.Application.Dto.xml");
                options.IncludeXmlComments(xmlPath);
            });

            //注册业务层
            services.AddSingleton<UserInfoManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(
              builder => builder
              .AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials()
            );

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "V1 Docs");
            });
        }
    }
}
