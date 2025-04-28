using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    public class User
    {
       
        public int Id { get; set; }
      
        public string FirstName { get; set; }
       
        public string LastName { get; set;}
        
        public string Email { get; set; }
        
        public byte[] PasswordHash { get; set; }
         
        public byte[] PasswordSalt { get; set; }
         
        public bool IsActive { get; set; } 

        public DateTime Created { get; set; } = DateTime.Now;

       
        public int RoleId { get; set; }

        public Role Role { get; set; }
    }
}
