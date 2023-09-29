using Bogus;

namespace ShopNow.UnitTests.Setup
{
    public class BaseTest
    {
        public BaseTest()
        {
            Faker = new Faker("pt_BR");
        }

        protected Faker Faker { get; }
    }
}
