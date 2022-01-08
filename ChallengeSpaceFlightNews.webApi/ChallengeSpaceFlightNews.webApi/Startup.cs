using ChallengeSpaceFlightNews.webApi.Data;
using ChallengeSpaceFlightNews.webApi.Domains.Services;
using ChallengeSpaceFlightNews.webApi.Interfaces;
using ChallengeSpaceFlightNews.webApi.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeSpaceFlightNews.webApi
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

            services
               .AddControllers()
               .AddNewtonsoftJson(options =>
               {
                   // Ignora loops infinitos
                   options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

                   // Ignora valores nulos
                   options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
               });
            

            services.AddDbContext<SpaceFlightNewsContext>(x => x.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));



            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ChallengeSpaceFlightNews.webApi", Version = "v1" });
            });

            services.AddCors(options => {
                options.AddPolicy("CorsPolicy",
                    builder => {
                        builder.AllowAnyOrigin()
                                                                    .AllowAnyHeader()
                                                                    .AllowAnyMethod();
                    }
                );
            });

            #region Injeções de dependência
            services.AddTransient<IArticleRepository, ArticleRepository>();
            services.AddTransient<IArticleService, ArticleService>();
            #endregion

            services.AddAutoMapper(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ChallengeSpaceFlightNews.webApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors("CorsPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
