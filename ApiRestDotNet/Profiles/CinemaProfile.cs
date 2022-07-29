using ApiRestDotNet.Data.Dtos.Cinema;
using ApiRestDotNet.Models;
using AutoMapper;

namespace ApiRestDotNet.Profiles
{
    public class CinemaProfile: Profile
    {
        public CinemaProfile()
        {
            CreateMap<CreateCinemaDto, Cinema>();
            CreateMap<Cinema, ReadCinemaDto>();
            CreateMap<UpdateCinemaDto, Cinema>();
        }
    }
}