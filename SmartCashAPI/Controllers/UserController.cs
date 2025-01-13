using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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
        public async Task<ActionResult<string>> Login([FromBody] LoginModel login)
        {
            IdentityUser? user = await _context.Users.SingleOrDefaultAsync(user => user.Email == login.Email);

            if (user == null)
            {
                return BadRequest("User does not exist");
            }

            if (user.PasswordHash == null) throw new Exception("No password hash found for user");

            PasswordVerificationResult passwordsMatch = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, login.Password);

            if (passwordsMatch == PasswordVerificationResult.Failed)
            {
                return BadRequest("Wrong password");
            }

            var tokenHandler = new JwtSecurityTokenHandler();

            if (_configuration["Jwt:Key"] == null) throw new Exception("Hash key not found");

            string? configurationKey = _configuration["Jwt:Key"];

            if (configurationKey == null) throw new Exception("No key has been found in configuration");

            byte[] key = Encoding.ASCII.GetBytes(configurationKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim("userId", user.Id)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel register)
        {
            bool userExists = await _context.Users.AnyAsync(user => user.Email == register.Email);

            if (userExists)
            {
                return BadRequest("A user with that email already exists");
            }

            IdentityUser newUser = new IdentityUser()
            {
                Email = register.Email
            };

            newUser.PasswordHash = _passwordHasher.HashPassword(newUser, register.Password);

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return Ok("Registered successfully");

        }



        public class RegisterModel
        {
            public string Email { get; set; }
            public string Password { get; set; }

            public RegisterModel()
            {
                Email = "defaultEmail";
                Password = "defaultPassword";
            }

            public RegisterModel(string email, string password, string username)
            {
                Email = email;
                Password = password;
            }
        }

        public class LoginModel
        {
            public string Email { get; set; }
            public string Password { get; set; }

            public LoginModel()
            {
                Email = "defaultEmail";
                Password = "defaultPassword";
            }

            public LoginModel(string email, string password)
            {
                Email = email;
                Password = password;
            }
        }
    }
}