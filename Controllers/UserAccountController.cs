using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using _334_group_project_web_api.Models;
using MongoDB.Bson;
using System.Text;
using Newtonsoft.Json;
using RestSharp;
using _334_group_project_web_api.Helpers;
using Microsoft.Graph;

namespace _334_group_project_web_api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserAccountController : ControllerBase
    {
        private readonly UserAccountService _userAccountService;
        private readonly JwtService _jwtService;

        public UserAccountController(UserAccountService userAccountService, JwtService jwtService)
        {
            _userAccountService = userAccountService;
            _jwtService = jwtService;
        }

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

        public class UserCredentials
        {
            public string email { get; set; }
            public string password { get; set; }
        }

        [HttpPost]
        public async Task<ActionResult<UserAccount>> Validate([FromBody] UserCredentials credentials)
        {
            var hashedPassword = HashPassword(credentials.password);
            var user = await _userAccountService.GetAsyncByCreds(credentials.email, hashedPassword);

            if (user is null)
            {
                var errorResult = new
                {
                    Code = 403,
                    Message = "Credentials are invalid"
                };

                return Unauthorized(errorResult);
            }
            else
            {
                var jwt = _jwtService.Generate(user.Id);

                Response.Cookies.Append("334_group_token", jwt, new CookieOptions
                {
                    HttpOnly = true
                });

                var successResult = new
                {
                    User = user,
                    Code = 200
                };

                return Ok(successResult);
            }
        }

        [HttpGet]
        public async Task<List<UserAccount>> Get() =>
        await _userAccountService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<UserAccount>> Get(string id)
        {
            var user = await _userAccountService.GetAsync(id);

            if (user is null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpGet("GetById")]
        public async Task<ActionResult<UserAccount>> GetById()
        {
            if (Request.Cookies["ecommerce_centre_jwt"] is not null)
            {
                var errorResult = new
                {
                    Code = 403,
                    Message = "Credentials are invalid"
                };

                return Unauthorized(errorResult);
            }
            else
            {
                //var id = _jwtService.Decode(Request.Cookies["ecommerce_centre_jwt"]);
                var id = "6645e7b34a5f82fec3f4b448";
                var user = await _userAccountService.GetAsync(id);

                if (user is null)
                {
                    return NotFound();
                }

                var returnedUser = await _userAccountService.GetAsync(id);

                return Ok(returnedUser);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(UserAccount newUser)
        {
            newUser.Password = HashPassword(newUser.Password);

            await _userAccountService.CreateAsync(newUser);

            return CreatedAtAction(nameof(Get), new { id = newUser.Id }, newUser);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, UserAccount updatedUser)
        {
            var user = await _userAccountService.GetAsync(id);

            if (user is null)
            {
                return NotFound();
            }

            updatedUser.Id = user.Id;
            updatedUser.Password = user.Password;

            await _userAccountService.UpdateAsync(id, updatedUser);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userAccountService.GetAsync(id);

            if (user is null)
            {
                return NotFound();
            }

            await _userAccountService.RemoveAsync(id);

            return NoContent();
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
