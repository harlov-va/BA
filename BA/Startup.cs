using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using BA.DAL;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;
using AspNetCoreRateLimit;

namespace BA
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
            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            var connection = @"Server=(local);Database=BA;Trusted_Connection=True;ConnectRetryCount=0";
            services.AddDbContext<BAContext>(options => options.UseSqlServer(connection));
            services.AddAntiforgery(x => x.HeaderName = "X-XSRF-TOKEN");

            #region Защита от DDOS-атак
            // needed to load configuration from appsettings.json
            services.AddOptions();

            // needed to store rate limit counters and ip rules
            services.AddMemoryCache();

            //load general configuration from appsettings.json
            services.Configure<ClientRateLimitOptions>(Configuration.GetSection("ClientRateLimiting"));

            //load client rules from appsettings.json
            services.Configure<ClientRateLimitPolicies>(Configuration.GetSection("ClientRateLimitPolicies"));

            // inject counter and rules stores
            services.AddSingleton<IClientPolicyStore, MemoryCacheClientPolicyStore>();
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            #endregion

            services.AddMvc();
            services
            .AddMvc()
            .AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver =
                    new Newtonsoft.Json.Serialization.DefaultContractResolver();
            });

            //services.AddSingleton<IRepository, Repository>();
            services.AddScoped(typeof(IRepository), typeof(Repository));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IAntiforgery antiforgery)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            #region Защита от CSRF атак
            app.Use(next => context =>
            {
                var tokens = antiforgery.GetAndStoreTokens(context);
                context.Response.Cookies.Append("XSRF-TOKEN", tokens.RequestToken,
                new CookieOptions() { HttpOnly = false });
                return next(context);
            });
            #endregion
            #region DDOS вторая вставка
            app.UseClientRateLimiting();
            #endregion

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseCors(
                options => options.WithOrigins("http://localhost:8080").AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials()
            );
            app.UseMvc();
        }
    }
}
