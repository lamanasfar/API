using API.Context;
using API.DTOs.RegisterDtos;
using API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Helpers
{
    public class AuthHelper
    {
        private readonly LibraryContext _context;
        private readonly IConfiguration _configuration;
        public AuthHelper(LibraryContext context,IConfiguration configuration)
        {
            _configuration = configuration;
            _context = context;
        }

        public string CreateToken(string email, int id)
        {
            var key = _configuration["JWT:Key"];

            var issuer = _configuration["JWT:Issuer"];
            var audience = _configuration["JWT:Audience"];

            var user = _context.Users
                     .Include(u => u.Role) // Role ilə yüklə
                     .FirstOrDefault(u => u.Email == email);
            if (user == null)
                throw new Exception("User not found");


            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.Email,email),
                new Claim(ClaimTypes.NameIdentifier, id.ToString()),
                new Claim(ClaimTypes.Role,user.Role.Name)
                

            };

         

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }


}
