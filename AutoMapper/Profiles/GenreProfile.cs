using ProjWebProgramming.Models;

namespace ProjWebProgramming.AutoMapper.Profiles
{
    public class GenreProfile
    {
        public Guid GenreId { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
