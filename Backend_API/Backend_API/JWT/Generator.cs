using Backend_API.DTO;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Backend_API.JWT
{
    public class Generator
    {
        private readonly IConfiguration _config;

        public Generator(IConfiguration config)
        {
            _config = config;
        }
        public string tokenGenerator(UserDTO userDetails)
        {//We will use userId, username and firstName as payload for generating the JWTToken, but the reason for not using the lastName in payload due to it's repeating a lot

            Console.WriteLine(userDetails.firstName);//for debugging purpose
            var JWTSetting = _config.GetSection("JWTSettings");
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTSetting["SecretKey"]));
            var credentials=new SigningCredentials(secretKey,SecurityAlgorithms.HmacSha256);

            //claims
            var Claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,userDetails.id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email,userDetails.username),
                new Claim(JwtRegisteredClaimNames.Name,userDetails.firstName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            };
            var token = new JwtSecurityToken(
                issuer: JWTSetting["Issuer"],
                audience: JWTSetting["Audience"],
                claims: Claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(JWTSetting["ExpiresInMins"]?? "1060")),//default expiration time set to 60 if any problem occurs
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
