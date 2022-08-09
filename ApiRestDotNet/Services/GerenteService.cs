using System.Linq;
using ApiRestDotNet.Data;
using ApiRestDotNet.Data.Dtos.Gerente;
using ApiRestDotNet.Models;
using AutoMapper;
using FluentResults;

namespace ApiRestDotNet.Services
{
    public class GerenteService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public GerenteService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ReadGerenteDto AdicionaGerente(CreateGerenteDto dto)
        {
            var gerente = _mapper.Map<Gerente>(dto);
            _context.Gerentes.Add(gerente);
            _context.SaveChanges();
            return _mapper.Map<ReadGerenteDto>(gerente);
        }

        public ReadGerenteDto RecuperaGerentesPorId(int id)
        {
            var gerente = _context.Gerentes.FirstOrDefault(gerente => gerente.Id == id);
            if (gerente == null) 
                return null;
            var gerenteDto = _mapper.Map<ReadGerenteDto>(gerente);
            return gerenteDto;
        }

        internal Result DeleteGerente(int id)
        {
            var gerente = _context.Gerentes.FirstOrDefault(gerente => gerente.Id == id);
            if (gerente == null)
            {
                return Result.Fail("Gerente não encontrado");
            }
            _context.Remove(gerente);
            _context.SaveChanges();
            return Result.Ok();
        }
    }
}