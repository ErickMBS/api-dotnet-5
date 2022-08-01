using ApiRestDotNet.Data.Dtos.Gerente;
using ApiRestDotNet.Models;
using AutoMapper;

namespace ApiRestDotNet.Profiles
{
    public class GerenteProfile : Profile
    {
        public GerenteProfile()
        {
            CreateMap<CreateGerenteDto, Gerente>();
            CreateMap<Gerente, ReadGerenteDto>();
        }
    }
}