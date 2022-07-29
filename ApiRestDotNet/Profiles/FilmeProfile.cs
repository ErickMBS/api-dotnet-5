using ApiRestDotNet.Data.Dtos;
using ApiRestDotNet.Models;
using AutoMapper;

namespace ApiRestDotNet.Profiles
{
    public class FilmeProfile : Profile
    {
        public FilmeProfile()
        {
            CreateMap<CreateFilmeDto, Filme>();
            CreateMap<UpdateFilmeDto, Filme>();
            CreateMap<Filme, ReadFilmeDto>();
        }
    }
}