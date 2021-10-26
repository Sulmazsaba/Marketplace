using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Marketplace.Api;
using Marketplace.Domain;
using Marketplace.Domain.Repositories;
using Marketplace.Framework;
using Marketplace.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Raven.Client.Documents;
using Raven.Client.Documents.Operations.ConnectionStrings;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Marketplace
{
    public class Startup
    {
        private IConfiguration Configuration { get; }
        private IHostingEnvironment Environment { get; }
        public Startup(IHostingEnvironment environment, IConfiguration configuration)
        {
            Environment = environment;
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            const string connectionString = "Host=localhost;Database=Marketplace_Chapter8;username=ddd;password=book";

            // ravendb
            //var store = new DocumentStore()
            //{
            //    Urls = new[] { "http://localhost:8080" },
            //    Database = "Marketplace_Chapter8",
            //    Conventions =
            //    {
            //        FindIdentityProperty = m => m.Name == "DbId"
            //    }
            //};

            //store.Initialize();
            //services.AddScoped(c => store.OpenAsyncSession());
            services.AddEntityFrameworkNpgsql()
                .AddDbContext<ClassifiedAdDbContext>(options => options.UseNpgsql(connectionString));
            services.AddSingleton<ICurrencyLookup,FixedCurrencyLookup>();
            services.AddScoped<IUnitOfWork, RavenDbUnitOfWork>();
            services.AddScoped<IClassifiedAdRepository, ClassifiedAddRepository>();
            services.AddScoped<ClassifiedAdsApplicationService>();

            services.AddMvc(opt => opt.EnableEndpointRouting = false);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {

                        Title = "ClassifiedAds",
                        Version = "v1",

                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            app.EnsureDatabase();
            app.UseMvcWithDefaultRoute();
            app.UseSwagger(c =>
            {
                c.SerializeAsV2 = true;
            });
            app.UseSwaggerUI(c =>
            {

                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ClassifiedAds v1");
                c.RoutePrefix = string.Empty;
            });


        }
    }
}
