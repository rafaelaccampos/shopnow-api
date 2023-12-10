using Bogus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Respawn;
using Respawn.Graph;
using ShopNow.Infra;

namespace ShopNow.IntegrationTests.Setup
{
    public class DatabaseBase
    {
        protected static IServiceScope _scope;
        protected static ShopContext _context;
        protected Faker Faker { get; } = new Faker("pt_BR");

        [SetUp]
        public async Task SetUpScope()
        {
            _scope = TestEnvironment.Factory.Services.CreateScope();
            _context = TestEnvironment.Factory.Services.CreateScope().ServiceProvider.GetService<ShopContext>()!;

            var configuration = (IConfigurationRoot)TestEnvironment.Factory.Services.GetService(typeof(IConfiguration))!;
            var connectionString = configuration.GetConnectionString("Shops");

            var respawner = await Respawner.CreateAsync(connectionString, new RespawnerOptions
            {
                TablesToIgnore = new Table[]
                {
                    "VersionInfo",
                }
            });

            await respawner!.ResetAsync(connectionString);
        }

        protected static T GetService<T>()
        {
            return _scope.ServiceProvider.GetService<T>()!;
        }
    }
}