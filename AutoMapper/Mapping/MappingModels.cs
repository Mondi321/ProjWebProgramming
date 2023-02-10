using AutoMapper;
using ProjWebProgramming.AutoMapper.Profiles;
using ProjWebProgramming.Models;
using ProjWebProgramming.Models.DTOs;

namespace ProjWebProgramming.AutoMapper.Mapping
{
    public class MappingModels : Profile
    {
        public MappingModels()
        {
            CreateMap<Genre, GenreProfile>();
            CreateMap<Actor, ActorProfile>();
            CreateMap<User, UserProfile>();
            CreateMap<Director, DirectorProfile>();
            CreateMap<Movie, MovieDto>()
                .ForMember(d => d.Director, o => o.MapFrom(s => s.Director))
                .ForMember(d => d.Genres, o => o.MapFrom(s => s.Genres))
                .ForMember(d => d.Actors, o => o.MapFrom(s => s.Actors))
                .ForMember(d => d.Users, o => o.MapFrom(s => s.Users));

            CreateMap<TvShow, TvShowDto>()
                .ForMember(d => d.Director, o => o.MapFrom(s => s.Director))
                .ForMember(d => d.Genres, o => o.MapFrom(s => s.Genres))
                .ForMember(d => d.Actors, o => o.MapFrom(s => s.Actors))
                .ForMember(d => d.Users, o => o.MapFrom(s => s.Users));

            CreateMap<Movie, MovieProfile>();
            CreateMap<TvShow, TvShowProfile>();
            CreateMap<Genre, GenreDto>()
                .ForMember(d => d.Movies, o => o.MapFrom(s => s.Movies))
                .ForMember(d => d.TvShows, o => o.MapFrom(s => s.TvShows));

            CreateMap<MovieGenre, MovieGenreDto>()
                .ForMember(d => d.Movie, o => o.MapFrom(s => s.Movie));

            CreateMap<Review, ReviewDto>()
                .ForMember(d => d.User, o => o.MapFrom(s => s.User));
        }
    }
}
