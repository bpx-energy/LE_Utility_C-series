using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AFEDIVE.DataAccess.Interfaces.Respositories;
using AFEDIVE.DataAccess.Repositories;
using AutoMapper;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AFEDive.Common.Models;

namespace AFEDive.API
{
    public class Startup
    {
        private readonly ILogger _logger;
        public Startup(IConfiguration configuration, ILogger<Startup> logger)
        {
            Configuration = configuration;
            _logger = logger;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddTransient<IDrillingRepository>(drillingRepository => new DrillingRepository(this.Configuration.GetConnectionString("AFE")));
            services.AddTransient<IEventRepository>(eventRepository => new EventRepository(this.Configuration.GetConnectionString("AFE")));
            services.AddTransient<IUserRepository>(eventRepository => new UserRepository(this.Configuration.GetConnectionString("AFE")));
            //   services.AddTransient<IClaimsTransformation, ClaimsTransformer>();
            //   services.AddAuthentication(IISServerDefaults.AuthenticationScheme);
            services.AddAutoMapper(typeof(Startup));
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            services.AddApplicationInsightsTelemetry();
            _logger.LogInformation("Logging from ConfigureServices.");
            services.AddAuthentication(AzureADDefaults.JwtBearerAuthenticationScheme)
                .AddAzureADBearer(options => Configuration.Bind("AzureAd", options));

            //services.AddAuthentication(options =>
            //{
            //    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            //})
            //.AddJwtBearer(options =>
            //{
            //    options.Authority = "https://login.microsoftonline.com/79c3fa05-64e6-40a0-882c-2cb7fb4923c0/oauth2/v2.0/authorize";
            //    options.Audience = "api://84aea446-c1f9-474d-bd34-cb01b7b14200";
            //    options.RequireHttpsMetadata = false;
            //});
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });




            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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

            app.UseCors(policy =>
            {
                //TODO: This has to be addressed once we deploy both api and ui under the same domain
                policy.AllowAnyOrigin();
                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
                policy.WithExposedHeaders("WWW-Authenticate");
            });


            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
