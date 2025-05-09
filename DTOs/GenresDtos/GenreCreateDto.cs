﻿namespace API.DTOs.GenresDtos
{
    public class GenreGetDto
    {
        public int Id { get; set; }
        public string GenreName { get; set; }
    }
    public class GenreCreateDto
    {
        public string GenreName { get; set; }
        public bool IsActive { get; set; }
    }
    public class GenreUpdateDto
    {
        public string GenreName { get; set; }
    }
    public class GenreSortingDto
    {
        public int Id { get; set; }
        public string GenreName { get; set; }
    }
    public class GenreFilteringDto:GenreSortingDto
    {

    } 
}
