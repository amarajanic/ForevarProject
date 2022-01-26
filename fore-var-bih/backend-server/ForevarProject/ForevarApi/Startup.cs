using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.IO;
using System.Reflection;
using System.Web;

namespace ForevarApi
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
     


        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddCors(o => o.AddPolicy("_myAllowSpecificOrigins", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
            services.AddControllers();


            services.AddSwaggerGen(setupAction =>
            {
            setupAction.SwaggerDoc("ForevarAPIDocumentation", new OpenApiInfo()
            {
                Title = "Forevar API",
                Version = "1",
                Description = "API made for ForevarBIH internship project.",
                Contact = new OpenApiContact()
                {
                    Email = "amar.ajanic@edu.fit.ba",
                    Name = "Amar Ajanic",
                    Url = new System.Uri("https://mss.ba/")
                },
                License = new OpenApiLicense()
                {
                    Name = "MSS License",
                    Url = new System.Uri("https://mss.ba/")
                },
                TermsOfService = new System.Uri("https://mss.ba/")
            });
  
                setupAction.IncludeXmlComments(@"wwwroot/swagger/ForevarApi.xml");
                setupAction.IncludeXmlComments(@"wwwroot/swagger/ForevarLibrary.xml");
       
                setupAction.AddSecurityDefinition("Authorization", new OpenApiSecurityScheme
                {
                    Description = "Standard Authorization header.",

                    In = ParameterLocation.Header,

                    Name = "Authorization",

                    Type = SecuritySchemeType.ApiKey

                });

                setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Name = "Authorization",
                            Type = SecuritySchemeType.ApiKey,
                            In = ParameterLocation.Header,
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Authorization"
                            },
                        },
                        new string[]{}
                    }
                });

                setupAction.OperationFilter<SecurityRequirementsOperationFilter>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();

                app.UseHttpsRedirection();
            }


            app.UseRouting();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            app.UseSwagger();

            app.UseSwaggerUI(setupAction =>
            {
                setupAction.SwaggerEndpoint("/swagger/ForevarAPIDocumentation/swagger.json", "Forevar API");

                //setupAction.RoutePrefix = "";
            });
            app.UseStaticFiles();

        }






    }
}
