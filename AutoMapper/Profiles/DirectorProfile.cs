namespace ProjWebProgramming.AutoMapper.Profiles
{
    public class DirectorProfile
    {
        public Guid DirectorId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Nationality { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public byte[]? Image { get; set; }
    }
}
