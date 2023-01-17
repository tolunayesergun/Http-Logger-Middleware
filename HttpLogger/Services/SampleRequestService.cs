using HttpLogger.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net.Http.Headers;

namespace HttpLogger.Services
{
    public class SampleRequestService : ISampleRequestService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public SampleRequestService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public async Task<string> GetDataFromAPI()
        {
            using var client = new HttpClient();
            try
            {
                client.BaseAddress = new Uri("https://dummyjson.com/");

                client.AddHeaders(_contextAccessor);

                var response = await client.GetAsync("auth/resource");

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();

                    return result;
                }
                else
                {
                    return $"Error: {response.StatusCode}";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

    }
    public static class HttpClientExtensions
    {
        public static void AddHeaders(this HttpClient client, IHttpContextAccessor contextAccessor)
        {
            var token = contextAccessor?.HttpContext?.Request.Headers["Authorization"].ToString();

            if (string.IsNullOrEmpty(token)) throw new UnauthorizedAccessException("Token Not Found");

            (string schema, string token) auth = (schema: token?.Split(" ")[0].ToString() ?? "", token: token?.Split(" ")[1] ?? "");

            string contentType = "application/json";
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(auth.schema, auth.token);
        }
    }
}