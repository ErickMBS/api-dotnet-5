using ApiRestDotNet.Data.Dtos.Endereco;
using ApiRestDotNet.Models;
using AutoMapper;

namespace ApiRestDotNet.Profiles
{
    public class EnderecoProfile : Profile
    {
        public EnderecoProfile()
        {
            CreateMap<CreateEnderecoDto, Endereco>();
            CreateMap<UpdateEnderecoDto, Endereco>();
            CreateMap<Endereco, ReadEnderecoDto>();
        }
    }
}