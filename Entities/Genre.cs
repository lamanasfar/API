namespace API.Entities
{
    public class Genre
    {
        public int Id { get; set; }
        public string GenreName { get; set; }
        public bool IsActive { get; set; }

        public List<Book> Books { get; set; } = new List<Book>();
    }
}
