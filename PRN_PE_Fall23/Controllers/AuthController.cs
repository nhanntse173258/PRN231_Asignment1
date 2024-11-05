using BOs.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PRN_PE_Fall23.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;
        private readonly IAccountRepo _accountRepo;

        public AuthController(IOptions<JwtSettings> jwtSettings, IAccountRepo accountRepo)
        {
            _jwtSettings = jwtSettings.Value;
            _accountRepo = accountRepo;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = _accountRepo.GetAccount(request.Email, request.Password);
            if (user != null)
            {
                var token = GenerateJwtToken(user);
                return Ok(new
                {
                    token,
                    role = GetRoleName(user.Role.Value)
                });
            }
            return Unauthorized();
        }

        private string GenerateJwtToken(BranchAccount user)
        {
            string roleName = GetRoleName(user.Role.Value);
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.FullName), // User email as subject
                new Claim(ClaimTypes.Name, user.FullName),              // Store username/email
                new Claim(ClaimTypes.Role, roleName),               // Store role
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // Token ID
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtSettings.AccessTokenExpiration),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private string GetRoleName(int roleId)
        {
            return roleId switch
            {
                1 => "Admin",
                2 => "Member",
                3 => "Manager", 
                4 => "Staff",
                _ => "Guest"
            };
        }
    }
}
