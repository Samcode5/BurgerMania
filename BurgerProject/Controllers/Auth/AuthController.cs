using BurgerProject.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BurgerProject.Data;
using BurgerProject.Model;
using Microsoft.EntityFrameworkCore;

namespace BurgerProject.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context; // Assuming you have a DbContext
        private readonly JwtSettings _jwtSettings;

        public AuthController(AppDbContext context, JwtSettings jwtSettings)
        {
            _context = context;
            _jwtSettings = jwtSettings; // Get the value of JwtSettings
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Number == request.Number && u.Password == request.Password);
            if (request == null || string.IsNullOrEmpty(request.Number) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("Invalid login request");
            }

            if (user == null)
                return Unauthorized();
            // JwtSettings jwtSettings = new JwtSettings();
            if (string.IsNullOrEmpty(_jwtSettings.SecretKey))
            {
                throw new ArgumentNullException(nameof(_jwtSettings.SecretKey), "SecretKey must be configured.");
            }




            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.Name, user.Number),
                new Claim(ClaimTypes.Role, user.Role),
            }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };



            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);
            return Ok(new { Token = jwtToken });
        }
    }
}

public class LoginRequest
{
    public string Number { get; set; }
    public string Password { get; set; }
}
