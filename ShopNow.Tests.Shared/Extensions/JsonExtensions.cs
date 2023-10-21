using FluentAssertions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System.Net.Mime;
using System.Text;

namespace ShopNow.Tests.Shared.Extensions
{
    public static class JsonExtensions
    {
        public static HttpContent ToJsonContent(this object obj) 
        {
            return new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, mediaType: MediaTypeNames.Application.Json);
        }

        public static void ShouldBeAnEquivalentJson(this string actual, string expected) 
        {
            var actualJToken = actual.ToJToken();
            var expectedJToken = expected.ToJToken();
            var areEquals = JToken.DeepEquals(actualJToken, expectedJToken);
            areEquals.Should().BeTrue();
        }

        public static string Serialize(this object obj)
        {
            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                FloatFormatHandling = FloatFormatHandling.DefaultValue,
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                }
            };
            return JsonConvert.SerializeObject(obj, settings);
        }

        private static JToken ToJToken(this string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentException("The JSON sent to the JToken() method is empty or is null.");
            }

            return JToken.Parse(text);
        }
    }
}
