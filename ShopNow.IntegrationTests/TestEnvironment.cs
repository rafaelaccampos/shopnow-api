using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestPlatform.TestHost;

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
