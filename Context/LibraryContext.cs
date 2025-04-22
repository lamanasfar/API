using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Context
{
    public class LibraryContext :DbContext
    {
        public LibraryContext (DbContextOptions <LibraryContext> options) : base(options)
        {
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre > Genres { get; set; }   
        public DbSet<User> Users { get; set; }

    }
}
