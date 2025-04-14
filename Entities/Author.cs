namespace API.Entities
{
    public class Author
    {
        public Guid Id { get; set; }
        public string AuthName { get; set; }
        public bool IsActive  { get; set; }

        public List<Book> Books { get; set; } = new List<Book>();
    }
}
