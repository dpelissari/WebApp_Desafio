using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace WebApp_Desafio_API
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, IConfiguration configuration)
        {
            Configuration = configuration;
            CurrentEnvironment = env;
        }

        public IConfiguration Configuration { get; }

        public IHostingEnvironment CurrentEnvironment { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddHttpContextAccessor()
                .AddMvc()
                .AddViewOptions(options => options.HtmlHelperOptions.ClientValidationEnabled = true)
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(options => options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)
                .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver())
                .AddJsonOptions(options => options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter()));

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new Swashbuckle.AspNetCore.Swagger.Info()
                    {
                        Version = "v1",
                        Title = "WebApp_Desafio_API",
                        Description = "API de comunicação WebApp_Desafio_Desenvolvimento.",
                        Contact = new Swashbuckle.AspNetCore.Swagger.Contact()
                        {
                            Name = "Netspeed",
                            Email = "contato@netspeed.com.br",
                            Url = "https://netspeed.com.br"
                        }
                    });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                if (File.Exists(xmlFile))
                {
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    c.IncludeXmlComments(xmlPath);
                }
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            string swaggerPath = "/swagger/{documentname}/WebApp_Desafio_API.json";

            app.UseSwagger(c =>
            {
                c.RouteTemplate = swaggerPath;
            });
            
            swaggerPath = swaggerPath.Replace("{documentname}", "v1");

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(swaggerPath, "WebApp_Desafio_API V1");
                c.RoutePrefix = "";
            });
            
        }
    }
}
