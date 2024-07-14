using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Writers;
using SmartCashAPI.Models;

namespace SmartCashAPI.Controllers
{
    [Route("api/[controller]")]

    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordHasher<IdentityUser> _passwordHasher;
        private readonly IConfiguration _configuration;

        public UserController(ApplicationDbContext context, IPasswordHasher<IdentityUser> passwordHasher, IConfiguration configuration)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _configuration = configuration;
        }




        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(string username, string password)
        {
            IdentityUser? user = await _context.Users.SingleOrDefaultAsync(user => user.UserName == username);

            if (user == null)
            {
                return Ok();

            }

            if (user.PasswordHash == null) throw new Exception("No password hash found for user");

            PasswordVerificationResult passwordsMatch = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);

            if (passwordsMatch == PasswordVerificationResult.Failed)
            {
                return BadRequest("Wrong password");
            }

            var tokenHandler = new JwtSecurityTokenHandler();

            if (_configuration["Jwt:Key"] == null) throw new Exception("Hash key not found");

            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Name, user.UserName)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }



        public class RegisterModel
        {
            public string? Email { get; set; }
            public string? Password { get; set; }
            public string? Username { get; set; }
        }

        public class LoginModel
        {
            public string? EmailOrUsername { get; set; }
            public string? Password { get; set; }
        }
    }
}