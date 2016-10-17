using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using BalanceApi.Configuration;
using BalanceApi.Services;
using BalanceApi.Model.Data;
using BalanceApi.Model.Data.Dapper;

namespace BalanceApi
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; set; }

        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddLogging();

            // Application services
            services.AddSingleton<AppSettingsHelper, AppSettingsHelper>();
            services.AddSingleton<IAccountTypeDao, AccountTypeDao>();

            services.AddSingleton<AccountTypeService, AccountTypeService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            // app.UseIISPlatformHandler();

            // app.UseStaticFiles();

            app.UseMvc();
        }
    }
}
