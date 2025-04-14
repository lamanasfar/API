using API.Entities;
using API.Interfaces;

namespace API.Repositories
{
    public class GenreFilterRepository :IBookFilterRepository
    {
        //private readonly Guid? _genreId;

        //public GenreFilterRepository(Guid? genreId)
        //{
        //    _genreId = genreId;
        //}

        //public IQueryable<Book> Filtering(IQueryable<Book> books) { 

        //    return _genreId.HasValue ? books.Where(b => b.GenreId == _genreId) : books;
        //}
        
    }
}
