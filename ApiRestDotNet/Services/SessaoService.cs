using System.Linq;
using ApiRestDotNet.Data;
using ApiRestDotNet.Data.Dtos.Sessao;
using ApiRestDotNet.Models;
using AutoMapper;

namespace ApiRestDotNet.Services
{
    public class SessaoService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public SessaoService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ReadSessaoDto AdicionaSessao(CreateSessaoDto dto)
        {
            Sessao sessao = _mapper.Map<Sessao>(dto);
            _context.Sessoes.Add(sessao);
            _context.SaveChanges();
            return _mapper.Map<ReadSessaoDto>(sessao);
        }

        public ReadSessaoDto RecuperaSessoesPorId(int id)
        {
            var sessao = _context.Sessoes.FirstOrDefault(sessao => sessao.Id == id);
            return sessao != null
                ? _mapper.Map<ReadSessaoDto>(sessao)
                : null;
        }
    }
}