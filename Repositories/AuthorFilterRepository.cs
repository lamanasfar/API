using API.Entities;
using API.Interfaces;

namespace API.Repositories
{
    public class AuthorFilterRepository : IBookFilterRepository
    {
        //private readonly Guid? _authorId;
        //public AuthorFilterRepository(Guid? authorId)
        //{
        //    _authorId = authorId;
             
        //}

        //public IQueryable<Book> Filtering(IQueryable<Book> books)
        //{
        //    return _authorId.HasValue ? books.Where(b => b.AuthorId == _authorId) : books;
            
        //}
    }
}
