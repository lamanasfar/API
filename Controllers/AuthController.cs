using API.Context;
using API.DTOs.AuthsDtos;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
     
    public class AuthController : ControllerBase
    {
        private readonly LibraryContext _context;
        
        public AuthController(LibraryContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAuth()
        {
            List<AuthGetDto> author = await _context.Authors.Where(s => s.IsActive == true).Select(s => new AuthGetDto
            {
                Id = s.Id,
                AuthName = s.AuthName
            }).ToListAsync();
            return Ok(author);

        }
        #region sorting2
        //[HttpGet("sorting")]
        //public async Task<IActionResult> GetAuthSorting([FromQuery] sbyte sortValue)
        //{
        //    List<AuthGetDto> authorSorting = await _context.Authors.Where(s => s.IsActive == true).Select(s => new AuthGetDto
        //    {
        //        Id = s.Id,
        //        AuthName = s.AuthName
        //    }).ToListAsync();




        //    switch (sortValue)
        //    {
        //        case 0:
        //            break;

        //        case 1:
        //            authorSorting.Sort((x, y) => x.AuthName.CompareTo(y.AuthName));
        //            break;
        //        case -1:
        //            authorSorting.Reverse();
        //            break;
        //        default:
        //            break;

        //    }



        //     return Ok(authorSorting);

        //}


        #endregion
        [HttpGet("filtering")]

        public async Task<IActionResult> GetAuthFiltering(string name)
        {
            List<AuthFilteringDto> authFiltering = await _context.Authors.Where(s => s.IsActive == true && s.AuthName.Contains(name)).Select(s => new AuthFilteringDto
            {
                Id = s.Id,
                AuthName = s.AuthName
            }).ToListAsync();
            return Ok(authFiltering);
        }

        [HttpGet("sorting")]
        public async Task<IActionResult> GetAuthSorting(sbyte sortValue)
        {
            List<AuthSortingDto> authorSorting = await _context.Authors.Where(s => s.IsActive == true).Select(s => new AuthSortingDto
            {
                Id = s.Id,
                AuthName = s.AuthName
            }).   //(s => s.AuthName).


            ToListAsync();



            switch (sortValue)
            {
                case 0:
                    authorSorting = authorSorting.OrderByDescending(s => s.Id).ToList();
                    break;
                case 1:
                    authorSorting = authorSorting.OrderBy(s => s.AuthName).ToList();
                    break;
                case -1:
                    authorSorting = authorSorting.OrderByDescending(s => s.AuthName).ToList();
                    break;
                default:
                    break;


            }


            return Ok(authorSorting);

        }


        [HttpPost]
        public async Task<ActionResult> CreateAuth([FromBody] AuthCreateDto authCreateDto)
        {
            if ( await _context.Authors.AnyAsync(i => i.AuthName.ToLower() == authCreateDto.AuthName.ToLower()))
                return BadRequest("Author is exists!");
          
            var newauth = new Author()
            {
                AuthName = authCreateDto.AuthName,
                IsActive = authCreateDto.IsActive
            };
            
            await _context.AddAsync(newauth);
            await _context.SaveChangesAsync();
            return Ok(newauth); 
        }

        

        [HttpPut]
        public async Task<ActionResult> UpdateAuth(Guid id, [FromBody] AuthUpdateDto authUpdateDto)
        {
            var updatedAuth = await _context.Authors.FirstOrDefaultAsync(s => s.Id == id);
            if (updatedAuth == null)
            {
                return NotFound(new { message = "Id not found" });
            }

            updatedAuth.AuthName = authUpdateDto.AuthName;  

            _context.Entry(updatedAuth).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(updatedAuth);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult>DeleteAuth(Guid id)
        {
            var auth = await _context.Authors.FindAsync(id);
            if (auth == null)
            {
                return NotFound(new { message = "Id not found" });
            }
            auth.IsActive = false; 
            _context.Authors.Remove (auth);
            await _context.SaveChangesAsync();
            return Ok("Deleted");
        }

        
    }
}
