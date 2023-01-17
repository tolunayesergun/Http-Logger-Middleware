using HttpLogger.Objects;
using HttpLogger.Services.Interfaces;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HttpLogger.Services
{
    public class AuthService: IAuthService
    {
        public async Task<string> GetToken()
        {
            using var client = new HttpClient();
            try
            {
                client.BaseAddress = new Uri("https://dummyjson.com/");

                string contentType = "application/json";
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));
                var myContent = JsonConvert.SerializeObject(new { username = "kminchelle", password = "0lelplR" });
                var stringContent = new StringContent(myContent, UnicodeEncoding.UTF8, "application/json");

                var response = await client.PostAsync("auth/login", stringContent);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<TokenResponse>();

                    return  result != null ? result.Token : string.Empty;
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
}
