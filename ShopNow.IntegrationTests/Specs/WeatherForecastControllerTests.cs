using ShopNow.IntegrationTests.Setup;
using System.Net.Http.Json;

namespace ShopNow.IntegrationTests.Specs
{
    public class WeatherForecastControllerTests : ApiBase
    {
        [Test]
        public async Task ShouldBeAbleToRecoveryDataFromWeatherForecast()
        {
            var response = await _httpClient.GetFromJsonAsync<WeatherForecast[]>("weatherForecast");

            Assert.Equals(5, response!.Length);
        }
    }
}
