using API.Context;
using API.DTOs.RegisterDtos;
using API.Entities;
using API.Helpers;
using API.Validations.UserValidation;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
 

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<RegisterValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<LoginValidator>();

builder.Services.AddDbContext<LibraryContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")));
//builder.Services.AddIdentity<User,IdentityRole>(opt =>
//{
//    opt.Password.RequiredLength = 7;
//    opt.Password.RequireDigit = false;
//    opt.Password.RequireUppercase = false;



//}).AddEntityFrameworkStores<LibraryContext>();

builder.Services.AddScoped<AuthHelper>();
//builder.Services.AddScoped<IValidator<RegisterDto>, RegisterValidator>();
//builder.Services.AddScoped<IValidator<LoginDto>, LoginValidator>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
 

// JWT configuration
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuration["JWT:Issuer"],
            ValidAudience = configuration["JWT:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]))
        };
    });

//builder.Services.AddAuthorization();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
