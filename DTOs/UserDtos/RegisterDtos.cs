﻿using System.ComponentModel.DataAnnotations;

namespace API.DTOs.RegisterDtos

{
    public class RegisterDto
    {
       
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string Email { get; set; }
        
        public string Password { get; set; }

        public string RoleName { get; set; }
    }

    public class LoginDto
    {
       
        public string Email { get; set; }
        
        public string Password { get; set; }

        

    }

   
}
