namespace API.DTOs.BooksDtos
{
    public class BookCreateDto
    {
        public string BookName { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public DateTime PublishDate { get; set; }
        public Guid AuthId { get; set; }
        public Guid GenreId { get; set; }
        public bool IsActive { get; set; }
    }

    public class BookGetDto
    {
        public Guid Id { get; set; }
        public string BookName { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public DateTime PublishDate { get; set; }   
        public string AuthName { get; set; }
        public string GenreName { get; set; }
    }

    public class BookUpdateDto { 
        public string BookName { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public DateTime PublishDate { get; set; }
    }

    public class BookNameSortingDto
    {
        public Guid Id { get; set; }
        public string BookName { get; set; }
    }
    public class BookFilteringDto:BookNameSortingDto
    {

    }

    
    
    

}
