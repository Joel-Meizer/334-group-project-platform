using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace _334_group_project_web_api.Helpers
{
    public class JwtService
    {
        private string secureKey = "1970f389-d591-4e67-83dc-a8ab83abfb4a-68c7cc59-aea5-4595-9089-6be90f3e911f";
        public string Generate(string id)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secureKey));
            var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
            var header = new JwtHeader(credentials);
            var payload = new JwtPayload(id, null, null, null, DateTime.Now.AddDays(1)); //expires in 1 day
            var securitytoken = new JwtSecurityToken(header, payload);

            return new JwtSecurityTokenHandler().WriteToken(securitytoken);
        }

        public JwtSecurityToken Verify(string jwt)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secureKey);
            tokenHandler.ValidateToken(jwt, new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false
            }, out SecurityToken validatedToken); ;

            return (JwtSecurityToken)validatedToken;
        }
    }
}
