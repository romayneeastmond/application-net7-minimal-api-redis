using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text;

namespace ApplicationSearch.Services.Sites
{
    public static class SitesHttpClientHelper
    {
        public async static void PostData(object body, string endPoint)
        {
            try
            {
                var client = new HttpClient
                {
                    Timeout = new TimeSpan(0, 10, 0)
                };

                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri(endPoint),
                    Method = HttpMethod.Post
                };

                request.Headers.Add("Accept", "*/*");

                var serializerSettings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                var bodyString = JsonConvert.SerializeObject(body, serializerSettings);

                var content = new StringContent(bodyString, Encoding.UTF8, "application/json");

                request.Content = content;

                var response = await client.SendAsync(request);
                var result = await response.Content.ReadAsStringAsync();

                Console.WriteLine(result);
            }
            catch
            {
                //ignored
            }
        }
    }
}
