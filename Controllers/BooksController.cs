using API.Context;
using API.DTOs.BooksDtos;
using API.Entities;
using API.Interfaces;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : Controller
    {

        private readonly LibraryContext _context;
       
        public BooksController(LibraryContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public async Task<ActionResult> GetBook()
        {
            List<BookGetDto> book = await _context.Books.Where(s => s.IsActive == true).Select(s => new BookGetDto
            {
                Id = s.Id,
                BookName = s.BookName,
                Price = s.Price,
                Title = s.Title,
                PublishDate = s.PublishDate,
                AuthName = s.Author.AuthName,
                GenreName = s.Genre.GenreName
            }).ToListAsync();
            return Ok(book);
        }

        [HttpGet("sorting")]
        public async Task<ActionResult>GetSortingBooks(sbyte sortValue)
        {
            List<BookNameSortingDto> bookNameSorting = await _context.Books.Where(s => s.IsActive == true).Select(s => new BookNameSortingDto
            {
                Id = s.Id,
                BookName = s.BookName
            }).ToListAsync();

            switch (sortValue)
            {
                case 0:
                    bookNameSorting = bookNameSorting.OrderByDescending(s => s.Id).ToList();
                    break;
                case 1:
                    bookNameSorting = bookNameSorting.OrderBy(s => s.BookName).ToList();
                    break;
                case -1:
                    bookNameSorting = bookNameSorting.OrderByDescending(s => s.BookName).ToList();
                    break;
                default:
                    break;


            }
            return Ok(bookNameSorting);

        }
     

         


        [HttpGet("filtering")]
        public async Task<ActionResult> GetFilteredBooks(string name)
        {
            List<BookFilteringDto> filteringBooks = await _context.Books.Where(s => s.IsActive == true && s.BookName.Contains(name)).Select(s => new BookFilteringDto
            {
                Id = s.Id,
                BookName = s.BookName
            }).ToListAsync();
            return Ok(filteringBooks);
        }


        [HttpPost]
            public async Task<ActionResult> CreateBook([FromBody] BookCreateDto bookCreateDto)
            {
            if (await _context.Books.AnyAsync(b => b.BookName.ToLower() == bookCreateDto.BookName.ToLower()))
                return BadRequest("Book is exists!");

            var newbook = new Book()
            {
                Title = bookCreateDto.Title,
                PublishDate = bookCreateDto.PublishDate,
                AuthorId = bookCreateDto.AuthId,
                GenreId = bookCreateDto.GenreId,
                BookName = bookCreateDto.BookName,
                Price = bookCreateDto.Price,
                IsActive = true

            };
                await _context.AddAsync(newbook);
                await _context.SaveChangesAsync();
                return Ok(newbook);
            }

        [HttpPut]
        public async Task<ActionResult> UpdateBook(Guid id, [FromBody] BookUpdateDto bookUpdateDto)
        {
            var updatedBook = await _context.Books.FirstOrDefaultAsync(s => s.Id == id);
            updatedBook.BookName = bookUpdateDto.BookName;
            updatedBook.Title = bookUpdateDto.Title;
            updatedBook.PublishDate = bookUpdateDto.PublishDate;
            updatedBook.Price = bookUpdateDto.Price;

            _context.Entry(updatedBook).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return Ok(updatedBook);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBook(Guid id)
        {

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound(new { message = "Id not found" });
            }
            book.IsActive = false;
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return Ok("Deleted");

        }
        //[HttpGet("filter")]
        //public IActionResult GetFilteredBooks([FromQuery] string? bookName, [FromQuery] Guid? authorId, [FromQuery] Guid? genreId)
        //{
        //    IQueryable<Book> books = _context.Books;


        //    List<IBookFilterRepository> filters = new List<IBookFilterRepository>();
        //    if (!string.IsNullOrEmpty(bookName))
        //        filters.Add(new BookNameRepository(bookName));


        //    if (authorId != null)
        //        filters.Add(new AuthorFilterRepository(authorId));

        //    if (genreId != null)
        //        filters.Add(new GenreFilterRepository(genreId));


        //    foreach (var filter in filters)
        //    {
        //        books = filter.Filtering(books);
        //    }

        //    return Ok(books.ToList());






        }



    
}
