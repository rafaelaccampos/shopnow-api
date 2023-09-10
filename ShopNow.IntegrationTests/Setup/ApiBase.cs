namespace ShopNow.IntegrationTests.Setup
{
    public class ApiBase 
    {
        protected HttpClient _httpClient;

        [SetUp] 
        public void Setup()
        {
            _httpClient = TestEnvironment.Factory.CreateClient();
        }
    }
}
