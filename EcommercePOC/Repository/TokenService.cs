using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EcommercePOC.Models;
using EcommercePOC.RepositoryInterface;
using Microsoft.IdentityModel.Tokens;

namespace EcommercePOC.Repository
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration config;

        public TokenService(IConfiguration config)
        {
            this.config = config;
        }

        public string CreateToken(AppUser appUser)
        {
            var tokenKey = config["TokenKey"] ?? throw new Exception("cannot accesss the token key from appsettings"); // ?? null-colascing operator if else which if it is null expection gets executed

            if (tokenKey.Length < 64) throw new Exception("your token key needs to longer");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)); //SymmetricSecurityKey -----> which means for encoding and decoding we are using the same key -->which means for signing and verifying the token key

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier,appUser.Name)
            }; // holds the collection of claims , claims are the key value pairs which contains the information about the user like username, role etc, these claims are inclueded in the jwt token to validate the user is valid one or not ,claims are the information about the user used for before generating the token to validate the user 

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = creds
            };
            var tokenhandler = new JwtSecurityTokenHandler();
            var token = tokenhandler.CreateToken(tokenDescriptor);

            return tokenhandler.WriteToken(token);
        }
    }
}

