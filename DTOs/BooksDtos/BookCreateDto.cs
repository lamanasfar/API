namespace API.DTOs.BooksDtos
{
    public class BookCreateDto
    {
        public string BookName { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public DateTime PublishDate { get; set; }
        public int AuthId { get; set; }
        public int GenreId { get; set; }
        public bool IsActive { get; set; }
    }

    public class BookGetDto
    {
        public int Id { get; set; }
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
        public int Id { get; set; }
        public string BookName { get; set; }
    }
    public class BookFilteringDto:BookNameSortingDto
    {

    }

    
    
    

}
