using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using _334_group_project_web_api.Models;
using MongoDB.Bson;
using System.Text;
using Newtonsoft.Json;
using System;
using RestSharp;

namespace _334_group_project_web_api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserAccountController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> GetAccessToken(string username, string password)
        {
            using (HttpClient userAuthClient = new HttpClient())
            {
                var url = "https://ap-southeast-2.aws.services.cloud.mongodb.com/api/client/v2.0/app/data-vtlhy/auth/providers/local-userpass/login";

                var userBody = new
                {
                    username,
                    password
                };

                var content = new StringContent(JsonConvert.SerializeObject(userBody), Encoding.UTF8, "application/json");

                HttpResponseMessage response = await userAuthClient.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var responseObject = JsonConvert.DeserializeObject<dynamic>(jsonResponse);
                    var accessToken = responseObject.access_token.ToString();

                    var cookieOptions = new CookieOptions
                    {
                        Expires = DateTime.Now.AddDays(1),
                        HttpOnly = true
                    };

                    Response.Cookies.Append("APIKey", accessToken, cookieOptions);

                    return Ok(new { access_token = accessToken });
                }
                else
                {
                    return BadRequest("Failed to get access token.");
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> Get(string userID)
        {
            string apiKey = Request.Cookies["APIKey"];

            var client = new RestClient("https://ap-southeast-2.aws.data.mongodb-api.com/app/data-vtlhy/endpoint/data/v1/action/findOne");
            var request = new RestRequest();
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Access-Control-Request-Headers", "*");
            request.AddHeader("Authorization", $"Bearer {apiKey}");
            var body = @"{" + "" + @" ""collection"":""UserAccounts""," + "" + @" ""database"":""3340group-project""," + "" + @" ""dataSource"":""334Cluster""," + "" +@" ""projection"":{""friendlyId"": " + "\"" + userID + "\"" + "}" + "" +@"" + "" +@"}";
            request.AddStringBody(body, DataFormat.Json);
            RestResponse response = await client.PostAsync(request);
            return Ok(response.Content);
        }

        [HttpPost]
        public async Task<IActionResult> Post(UserAccount newAccount, string accessToken)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Content-Type", "application/json");
                client.DefaultRequestHeaders.Add("Access-Control-Request-Headers", "*");
                client.DefaultRequestHeaders.Add("api-key", "IQ329isRYRS6d0YyVdtq5O8Y8eaiH2UYT8nHRVohamgXUBxZ7PnRgV5SJFviUCB4");

                var jsonBody = new BsonDocument
                {
                    { "collection", "UserAccounts" },
                    { "database", "3340group-project" },
                    { "dataSource", "334Cluster" },
                    { "projection", new BsonDocument("_id", 1) }
                }.ToString();

                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                string url = "https://ap-southeast-2.aws.data.mongodb-api.com/app/data-vtlhy/endpoint/data/v1/action/findOne";

                HttpResponseMessage response = await client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Response: " + responseContent);
                }
                else
                {
                    Console.WriteLine("Request failed with status code: " + response.StatusCode);
                }
            }
            return Ok(newAccount);
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
    }
}
