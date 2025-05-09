﻿using API.Context;
using API.DTOs.RegisterDtos;
using API.Entities;
using API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    
    public class UserController : ControllerBase
    {
        private readonly LibraryContext _context;
        private readonly AuthHelper _authhelper;
        public UserController(LibraryContext context,AuthHelper authhelper)
        {
            _context = context;
            _authhelper = authhelper;
        }
        ////[Authorize(Roles = "User")]
        //[HttpPost("register")]
        //public async Task<ActionResult> Register([FromBody] RegisterDto registerDto)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);
        //    if (await _context.Users.AnyAsync(s => s.Email == registerDto.Email))
        //        return BadRequest("Email is alredy exists");

        //    PasswordHasher.CreatePasswordHash(registerDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

        //    var role = await _context.Roles.FirstOrDefaultAsync(s => s.Name == "User");
        //    if (role == null)
        //        return BadRequest("Role 'User' not found.");

        //    var user = new User
        //    {
        //        FirstName = registerDto.FirstName,
        //        LastName = registerDto.LastName,
        //        Email = registerDto.Email,
        //        PasswordHash = passwordHash,
        //        PasswordSalt = passwordSalt,
        //        IsActive = registerDto.IsActive,
        //       // RoleId = role.Id, // <-- Bunu əlavə et
        //    };

        //    //var user = new User
        //    //{
        //    //    FirstName = registerDto.FirstName,
        //    //    LastName = registerDto.LastName,
        //    //    Email = registerDto.Email,
        //    //    PasswordHash = passwordHash,
        //    //    PasswordSalt = passwordSalt,
        //    //    IsActive = registerDto.IsActive,


        //    //};
        //    //var role = await _context.Roles.FirstOrDefaultAsync(s => s.Name == "User");
        //    //if (role == null)
        //    //    return BadRequest("Role 'User' not found.");

        //    await _context.AddAsync(user);
        //    await _context.SaveChangesAsync();
        //    return Ok(new { Message = "User registered successfully." });

        //}
        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _context.Users.AnyAsync(s => s.Email == registerDto.Email))
                return BadRequest("Email is already exists");

            PasswordHasher.CreatePasswordHash(registerDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var role = await _context.Roles.FirstOrDefaultAsync(s => s.Name == registerDto.RoleName);
            if (role == null)
                return BadRequest("Role 'User' not found.");
            

            var user = new User
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                IsActive = true,
                RoleId = role.Id
            };  

            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
            return Ok(new { Message = "User registered successfully." });
        }


        
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var user = await _context.Users.FirstOrDefaultAsync(s => s.Email == loginDto.Email);
                 if (user == null)
                return Unauthorized("Invalid email or password.");
            if (!PasswordHasher.VerifyPasswordHash(loginDto.Password, user.PasswordHash, user.PasswordSalt))
                return Unauthorized("Invalid email or password.");

            var token = _authhelper.CreateToken(loginDto.Email, user.Id);

            return Ok(new {token,Message = "User logged in successfully." }); //Token = token



        }
    }
}
