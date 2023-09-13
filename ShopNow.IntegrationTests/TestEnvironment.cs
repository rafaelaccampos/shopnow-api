using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShopNow.Infra.Data;
using ShopNow.IntegrationTests.Setup;

namespace ShopNow.IntegrationTests
{
    [SetUpFixture]
    public class TestEnvironment
    {
        public static WebApplicationFactory<Program> Factory;

        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            Factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

                var databaseCreator = new DatabaseCreator();
                databaseCreator.CreateDatabase(configuration.GetConnectionString("Shops"));

                builder.UseConfiguration(configuration);
            });
        }

        [OneTimeTearDown]
        public void RunAfterAnyTests()
        {
            Factory.Dispose();
        }
    }
}
