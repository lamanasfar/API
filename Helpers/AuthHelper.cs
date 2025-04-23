﻿using API.DTOs.RegisterDtos;
using API.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Helpers
{
    public class AuthHelper
    {
        private readonly IConfiguration _configuration;
        public AuthHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string CreateToken(string email, int id)
        {
            //var key = _configuration["JWT:Key"];
            //var issuer = _configuration["JWT:Issuer"];
            //var audience = _configuration["JWT:Audience"];

            
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.Email,email),
                new Claim(ClaimTypes.NameIdentifier, id.ToString())
                 
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
