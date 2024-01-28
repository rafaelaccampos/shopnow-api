using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
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
                .AddJsonFile("appsettings.Test.json")
                .Build();

                DatabaseCreator.CreateDatabase(configuration.GetConnectionString("Shops"));
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
