using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Marketplace.Api;
using Marketplace.Contracts;
using Marketplace.Domain;
using Marketplace.Domain.Repositories;
using Marketplace.Framework;
using Marketplace.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Raven.Client.Documents;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using static Marketplace.Contracts.ClassifiedAds;

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
            var store = new DocumentStore()
            {
                Urls = new[] { "http://localhost:8080" },
                Database = "Marketplace_Chapter8",
                Conventions =
                {
                    FindIdentityProperty = m => m.Name == "_databaseId"
                }
            };

            store.Initialize();
            services.AddSingleton<ICurrencyLookup,FixedCurrencyLookup>();
            services.AddScoped(c => store.OpenAsyncSession());
            services.AddScoped<IUnitOfWork, RavenDbUnitOfWork>();
            services.AddScoped<IClassifiedAdRepository, ClassifiedAdRepository>();
            services.AddScoped<ClassifiedAdsApplicationService>();

            //services.AddSingleton(new ClassifiedAdsApplicationService());
            //services.AddSingleton<IEntityStore, RavenDbEntityStore>();
            //services.AddScoped<IHandleCommand<V1.Create>>(c =>
            //    new RetryingCommandHandler<V1.Create>(
            //        new CreateClassifiedAdHandler(
            //            c.GetService<RavenDbEntityStore>())));

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
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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
