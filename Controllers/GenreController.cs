﻿using API.Context;
using API.DTOs.GenresDtos;
using API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly LibraryContext _context;
        public GenreController(LibraryContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetGenre()
        {
            List<GenreGetDto> genre = await _context.Genres.Where(s => s.IsActive == true).Select(s => new GenreGetDto
            { 
                Id = s.Id,
                GenreName = s.GenreName

            }).ToListAsync();
            return Ok(genre);

            
        }
        [HttpGet("sorting")]
        public async Task<ActionResult> GetSortingGenre(sbyte sortValue)
        {
            List<GenreSortingDto> genreSorting = await _context.Genres.Where(s => s.IsActive == true).Select(s => new GenreSortingDto
            {
                Id = s.Id,
                GenreName = s.GenreName
            }).ToListAsync();

            switch (sortValue)
            {
                case 0:
                    genreSorting = genreSorting.OrderByDescending(s => s.Id).ToList();
                    break;
                case 1:
                    genreSorting = genreSorting.OrderBy(s => s.GenreName).ToList();
                    break;
                case -1:
                    genreSorting = genreSorting.OrderByDescending(s => s.GenreName).ToList();
                    break;
                default:
                    break;


            }
             

            return Ok(genreSorting);
        }

        [HttpGet("filtering")]
        public async Task<ActionResult> GetFilteringGenre(string name)
        {
            List<GenreFilteringDto> genreFiltering = await _context.Genres.Where(s => s.IsActive == true && s.GenreName.Contains(name)).Select(s => new GenreFilteringDto
            {
                Id = s.Id,
                GenreName = s.GenreName
            }).ToListAsync();

            return Ok(genreFiltering);
        }


        [HttpPost]
        public async Task<ActionResult> CreateGenre(GenreCreateDto genreCreateDto)
        {
             

            var newGenre = new Genre()
            {
                GenreName = genreCreateDto.GenreName,
                IsActive = genreCreateDto.IsActive
            };

            if (await _context.Genres.AnyAsync(i => i.GenreName.ToLower() == newGenre.GenreName.ToLower()))
                return BadRequest("Genre is exists!");


            await _context.AddAsync(newGenre);
            await _context.SaveChangesAsync();
            return Ok(newGenre);
        }
        [HttpPut]
        public async Task<ActionResult> UpdateGenre(int id, GenreUpdateDto genreUpdateDto)
        {
            var updatedGenre = await _context.Genres.FirstOrDefaultAsync(s => s.Id == id);
            if (updatedGenre == null)
            {
                return NotFound(new { message = "Id not found" });
            }
             updatedGenre.GenreName=genreUpdateDto.GenreName;
            _context.Entry(updatedGenre).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(updatedGenre);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGenre(Guid id)
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre == null)
            {
                return NotFound(new { message = "Id not found" });
            }
            genre.IsActive = false;
            await _context.SaveChangesAsync();
            return Ok("Deleted");
        }

    }
}
