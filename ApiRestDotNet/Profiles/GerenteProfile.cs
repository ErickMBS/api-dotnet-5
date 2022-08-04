using System.Linq;
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
            CreateMap<Gerente, ReadGerenteDto>()
                .ForMember(gerente => gerente.Cinemas, opts => opts
                    .MapFrom(gerente => gerente.Cinemas
                        .Select(c => new
                        {
                            c.Id, 
                            c.Nome, 
                            c.Endereco, 
                            c.EnderecoId
                        }))
                );
        }
    }
}