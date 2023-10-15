using System.Net.Mime;
using System.Text;
using System.Text.Json;

namespace ShopNow.Tests.Shared.Extensions
{
    public static class JsonExtensions
    {
        public static HttpContent ToJsonContent(this object obj) 
        {
            return new StringContent(JsonSerializer.Serialize(obj), Encoding.UTF8, mediaType: MediaTypeNames.Application.Json);
        }
    }
}
