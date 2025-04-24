namespace API.DTOs.AuthsDtos
{
    public class AuthGetDto
    {
        public Guid Id { get; set; }
        public string AuthName { get; set; }
    }
    public class AuthUpdateDto : AuthCreateDto
    {
       // public Guid Id { get; set; }


    }
    public class AuthCreateDto {
        public string AuthName { get; set;} 
        public bool IsActive { get; set; } 
    }

    public class AuthSortingDto
    {
        public Guid Id { get; set; }
        public string AuthName { get; set; }
    }
    public class AuthFilteringDto: AuthSortingDto
    {
        
    }
}
