using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace _334_group_project_web_api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FatSecretController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public FatSecretController()
        {
            _httpClient = new HttpClient();
        }

        [HttpGet]
        public async Task<IActionResult> GenerateSecretKey()
        {
            var byteArray = Encoding.ASCII.GetBytes("3da3af6b422f41ec96fa18074aaed0fe:823c0e4a905247ebae681f5e8ee4fb6e");
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            var values = new Dictionary<string, string>
            {
               { "scope", "basic" },
               { "grant_type", "client_credentials" }
            };
            var content = new FormUrlEncodedContent(values);

            var responseString = await CollectSecretKey(content);
            return Ok(responseString);
        }

        private async Task<string> CollectSecretKey(FormUrlEncodedContent content)
        {
            var response = await _httpClient.PostAsync("https://oauth.fatsecret.com/connect/token", content);
            var responseString = await response.Content.ReadAsStringAsync();
            return responseString;
        }
    }
}
