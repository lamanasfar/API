using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string BookName { get; set; }
        public double Price { get; set; }
        public DateTime PublishDate { get; set; }
        public Guid AuthorId { get; set; }
        public Author Author { get; set; }
        public Guid GenreId { get; set; }
        public Genre Genre { get; set; }
        public bool IsActive { get; set; }
         
        

    }
}
