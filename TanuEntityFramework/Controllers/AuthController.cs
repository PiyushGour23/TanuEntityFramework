using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TanuEntityFramework.Model;

namespace TanuEntityFramework.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public IConfiguration _configuration;
        public TanuDbContext _tanuDbcontext;

        public AuthController(IConfiguration configuration, TanuDbContext tanuDbContext)
        {
            _configuration = configuration;
            _tanuDbcontext = tanuDbContext;
        }

        [HttpPost]
        public async Task<IActionResult> Post(usercred user)
        {
            if (user != null && user.username != null && user.password != null) 
            {
                var data = await GetUser(user.username, user.password);
                var jwt = _configuration.GetSection("JWTSetting").Get<JWTSetting>();
                if (user != null) 
                {
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, jwt.subject),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("Id", user.Id.ToString()),
                        new Claim("UserName", user.username),
                        new Claim("Password", user.password)

                    };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.key));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                       jwt.issuer,
                       jwt.audience,
                        claims,
                        expires: DateTime.Now.AddMinutes(20),
                        signingCredentials: signIn
                    );
                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return BadRequest("Invalid Credentials");
                }
            }
            else
            {
                return BadRequest("Invalid Credentials");
            }
        }


        [HttpGet]
        public async Task<usercred> GetUser(string username, string password)
        {
            return await _tanuDbcontext.usercreds.FirstOrDefaultAsync(u => u.username == username && u.password == password);
        }
    }
}
