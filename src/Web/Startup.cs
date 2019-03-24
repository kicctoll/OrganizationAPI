using System;
using System.IO;
using System.Reflection;
using AutoMapper;
using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Infrastructure.Data;
using Infrastructure.Logging;
using Infrastructure.Utilities;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;
using Web.Middlewares;
using Web.Utilities;

namespace Web
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
            services.AddDbContext<ApplicationContext>(optionsBuilder =>
            {
                optionsBuilder.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"),
                    sqlServerOptions => sqlServerOptions.MigrationsAssembly("Web"));
            });

            services.AddSingleton(p => new FileJsonLogger(
                Configuration["Logging:Paths:Folder"],
                Configuration["Logging:Paths:Info"],
                Configuration["Logging:Paths:Errors"]
            ));

            services.AddScoped<DbContext, ApplicationContext>();
            
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IOrganizationService, OrganizationService>();
            services.AddTransient<ICountryService, CountryService>();
            services.AddTransient<IBusinessService, BusinessService>();
            services.AddTransient<IFamilyService, FamilyService>();
            services.AddTransient<IOfferingService, OfferingService>();
            services.AddTransient<IDepartmentService, DepartmentService>();
            services.AddTransient<FacebookAuthenticationService>();

            AddMapping(services);
            AddAuthentication(services);

            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My Web API", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseMiddleware<LoggingMiddleware>();
            //app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseHttpsRedirection();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseMvc();
        }

        private void AddMapping(IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(conf =>
            {
                conf.AddProfile<MappingProfile>();
            });

            var mapper = mapperConfig.CreateMapper();

            services.AddSingleton(mapper);
        }

        private void AddAuthentication(IServiceCollection services)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = JwtBearerDefaultOptions.ISSUER,

                ValidateAudience = true,
                ValidAudience = JwtBearerDefaultOptions.AUDIENCE,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = JwtBearerDefaultOptions.GetSecurityKey(),

                ValidateLifetime = true,
            };

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = true;
                    options.TokenValidationParameters = tokenValidationParameters;
                });
        }
    }
}
