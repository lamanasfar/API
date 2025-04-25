using System.ComponentModel.DataAnnotations;

namespace API.DTOs.RegisterDtos

{
    public class RegisterDto
    {
       
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string Email { get; set; }
        
        public string Password { get; set; }
    }

    public class LoginDto
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters.")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

    }

   
}
