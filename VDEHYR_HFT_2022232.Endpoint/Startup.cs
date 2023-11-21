using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VDEHYR_HFT_2022232.Logic.Interfaces;
using VDEHYR_HFT_2022232.Logic.Logics;
using VDEHYR_HFT_2022232.Models;
using VDEHYR_HFT_2022232.Repository.Database;
using VDEHYR_HFT_2022232.Repository.Interfaces;
using VDEHYR_HFT_2022232.Repository.Repositories;

namespace VDEHYR_HFT_2022232.Endpoint
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
            services.AddTransient<DogsDbContext>();

            services.AddTransient<IRepository<Owner>, OwnerRepository>();
            services.AddTransient<IRepository<Dog>, DogRepository>();
            services.AddTransient<IRepository<Breed>, BreedRepository>();

            services.AddTransient<IOwnerLogic, OwnerLogic>();
            services.AddTransient<IBreedLogic, BreedLogic>();
            services.AddTransient<IDogLogic, DogLogic>();
            services.AddTransient<IExtendMethods, ExtendMethodLogic>();

            services.AddSignalR();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "VDEHYR_HFT_2022232.Endpoint", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "VDEHYR_HFT_2022232.Endpoint v1"));
            }

            app.UseExceptionHandler(c => c.Run(async context =>
            {
                var exception = context.Features
                .Get<IExceptionHandlerPathFeature>()
                .Error;
                var response = new { Msg = exception.Message };
                await context.Response.WriteAsJsonAsync(response);
            }));

            app.UseCors(x => x
                  .AllowCredentials()
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .WithOrigins("http://localhost:21058"));

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
