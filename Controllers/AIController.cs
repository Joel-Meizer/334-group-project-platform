using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using static System.Net.WebRequestMethods;
using System;
using Newtonsoft.Json;

namespace _334_group_project_web_api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AIController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> GetAISpeech([FromBody] string message)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer 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");

                var messages = new List<object>
                {
                    new { role = "user", content = message }
                };

                var jsonBody = new
                {
                    model = "gpt-3.5-turbo",
                    messages,
                    temperature = 0.7
                };

                string json = JsonConvert.SerializeObject(jsonBody);
                HttpContent content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync("https://api.openai.com/v1/chat/completions", content);

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    return Ok(responseContent);
                }
                else
                {
                    return BadRequest(response.ReasonPhrase);
                }
            }
        }
    }
}
