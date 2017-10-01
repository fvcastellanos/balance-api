using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using BalanceApi.Services;
using BalanceApi.Model.Data;
using BalanceApi.Model.Data.Dapper;
using BalanceApi.Domain;

using System.IO;
using BalanceApi.Validators;
using BalanceApi.Model.Domain;

namespace BalanceApi
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; set; }

        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                // .SetBasePath(Directory.GetCurrentDirectory()) // Not completely sure if this gonna work on production
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("AppSettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                ;

            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Security services
            services.AddCors();

            // Add framework services.
            services.AddLogging();
            services.AddOptions();
            services.AddMvc();
            services.AddSwaggerGen();
            
            services.Configure<AppSettings>(x => Configuration.GetSection("AppSettings").Bind(x));

            // Data Repositories
            services.AddSingleton<IAccountTypeDao, AccountTypeDao>();
            services.AddSingleton<IProviderDao, ProviderDao>();
            services.AddSingleton<ITransactionTypeDao, TransactionTypeDao>();

            // Application services
            services.AddSingleton<AccountTypeService, AccountTypeService>();
            services.AddSingleton<ProviderService, ProviderService>();
            services.AddSingleton<TransactionTypeService, TransactionTypeService>();

            // Validation services
            services.AddSingleton<IModelValidator<Provider>, ProviderValidator>();
            services.AddSingleton<IModelValidator<TransactionType>, TransactionTypeValidator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUi();
        }
    }
}
