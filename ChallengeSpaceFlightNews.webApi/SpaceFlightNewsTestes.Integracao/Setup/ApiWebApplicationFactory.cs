using AutoMapper;
using ChallengeSpaceFlightNews.webApi.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ChallengeSpaceFlightNews.webApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceFlightNewsTestes.Integracao.Setup
{
    public class ApiWebApplicationFactory : WebApplicationFactory<Startup>, IDisposable
    {
        public SpaceFlightNewsContext Context { get; private set; }
        public IServiceScope Scope { get; private set; }
        public IMapper Mapper { get; private set; }
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration(config =>
            {
                var integrationAppSettings = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.Integration.json")
                    .Build();
                config.AddConfiguration(integrationAppSettings);
            });

            builder.ConfigureServices(services =>
            {
                Scope = services.BuildServiceProvider().CreateScope();
                Context = Scope.ServiceProvider.GetRequiredService<SpaceFlightNewsContext>();
                Mapper = Scope.ServiceProvider.GetRequiredService<IMapper>();
            });
        }

        // O código que vem aqui será executado depois de passar por todos os testes
        public new void Dispose()
        {
            Context.Database.EnsureDeleted();
            Context.Dispose();
            Scope.Dispose();
        }
    }
}
