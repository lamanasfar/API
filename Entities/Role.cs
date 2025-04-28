using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    public class Role
    {
       
        public int Id{ get; set; }
        public string Name { get; set; }
 
        public List<User> User { get; set; } = new List<User>();


    }
}
