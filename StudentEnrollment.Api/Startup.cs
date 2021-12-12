using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using StudentEnrollment.Api.Utils;
using StudentEnrollment.Core;
using StudentEnrollment.EFCore;
using StudentEnrollment.Entities;
using StudentEnrollment.Store;
using Newtonsoft.Json;
using StudentEnrollment.Core.Services;

namespace StudentEnrollment.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("localhost")));
            services.AddIdentity<RequestUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
          services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.Formatting = Formatting.Indented;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("Version", new OpenApiInfo { Title = "StudentEnrollment Apis", Version = "v1" });
            });


            services.ConfigureSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                   new OpenApiInfo
                   {
                       Title = "StudentEnrollment Apis",
                       Version = "v1",
                       Description = "StudentEnrollment Apis",
                   }
                );
            });

            services.AddDbContext<ApplicationDbContext>(options => { options.UseSqlServer(Configuration.GetConnectionString("localhost")); });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<IUploadService, UploadService>();



            services.AddScoped<Message>();

            services.AddHandlers();

            services.AddDomainServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }

            app.UseHttpsRedirection();

            app.UseSwagger(c => c.RouteTemplate = "StudentEnrollment/swagger/{documentName}/swagger.json");

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/StudentEnrollment/swagger/v1/swagger.json", "StudentEnrollment Apis V1");
                c.RoutePrefix = "StudentEnrollment";

            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
