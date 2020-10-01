using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Pokemon.Clients;
using Pokemon.Data;
using Pokemon.Services;

namespace Pokemon.Search
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
            services.AddControllers();
            
            services.AddScoped<IPokemonApiData, PokemonApiData>();
            services.AddScoped<IPokeApiAgent, PokeApiAgent>();
            services.AddScoped<IPokemonApiService, PokemonApiService>();

            services.AddScoped<IShakespeareApiData, ShakespeareApiData>();
            services.AddScoped<IShakespeareApiAgent, ShakespeareApiAgent>();
            services.AddScoped<IShakespeareApiService, ShakespeareApiService>();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Pokemon Search Engine",
                    Version = "v2",
                    Description = "Pokemon Search Engine",
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

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v2/swagger.json", "Pokemon Search Engine"));
        }
    }
}
