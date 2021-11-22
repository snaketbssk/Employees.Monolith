using Employees.Monolith.Api.Authentications.Handlers.Entities;
using Employees.Monolith.Api.Authentications.Models.Constants;
using Employees.Monolith.Configurations.Entities;
using Employees.Monolith.Configurations.Models.Branches.Entities;
using Employees.Monolith.DataLayer.Models.Contexts.Entities;
using Employees.Monolith.LogicLayer.Models.Constants;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SnakeTBs.Configurations.Entities;
using SnakeTBs.Services.Authentications.SchemeOptions.Entities;
using System.Linq;

namespace Employees.Monolith.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            AppConfiguration.Set();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(BranchConfiguration<RootBranch>.Instance.Root.Database.Connection));
            //
            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigins", builder => builder
                    .WithOrigins(BranchConfiguration<RootBranch>.Instance.Root.Cors.Origins.Select(v => v.Url).ToArray())
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                );
            });
            //
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = SchemeConstant.VALIDATE_X_TOKEN;
            })
                .AddScheme<
                    ValidateAuthenticationSchemeOptions,
                    ValidateAuthenticationHandler>(SchemeConstant.VALIDATE_X_TOKEN, op => { });
            //
            if (BranchConfiguration<RootBranch>.Instance.Root.Swagger.IsActive)
            {
                services.AddSwaggerGen(c =>
                {
                    c.IncludeXmlComments(AppSettingsConfiguration.Instance.Root.Swagger.XmlFile);
                    if (AppSettingsConfiguration.Instance.Root.Swagger.IsAuthentication)
                    {
                        c.AddSecurityDefinition(HeaderConstant.X_TOKEN, new OpenApiSecurityScheme
                        {
                            In = ParameterLocation.Header,
                            Description = $"Please insert {HeaderConstant.X_TOKEN} into field",
                            Name = HeaderConstant.X_TOKEN,
                            Type = SecuritySchemeType.ApiKey
                        });
                        c.AddSecurityRequirement(new OpenApiSecurityRequirement
                        {
                            {
                                new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Type = ReferenceType.SecurityScheme,
                                        Id = HeaderConstant.X_TOKEN
                                    }
                                },
                                new string[] { }
                            }
                        });
                    }
                });
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            if (BranchConfiguration<RootBranch>.Instance.Root.Swagger.IsActive)
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                });
            }

            app.UseCors("AllowOrigins");

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
