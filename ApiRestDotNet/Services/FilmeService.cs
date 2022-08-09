using System.Collections.Generic;
using System.Linq;
using ApiRestDotNet.Data;
using ApiRestDotNet.Data.Dtos;
using ApiRestDotNet.Models;
using AutoMapper;

namespace ApiRestDotNet.Services
{
    public class FilmeService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public FilmeService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public ReadFilmeDto AdicionaFilme(CreateFilmeDto filmeDto)
        {
            var filme = _mapper.Map<Filme>(filmeDto);
            _context.Filmes.Add(filme);
            _context.SaveChanges();
            return _mapper.Map<ReadFilmeDto>(filme);
        }

        public List<ReadFilmeDto> RecuperaFilmes(int? classificacaoEtaria)
        {
            List<Filme> filmes;
            if (classificacaoEtaria == null)
                filmes = _context.Filmes.ToList();
            else
                filmes = _context
                    .Filmes
                    .Where(filme => filme.ClassificacaoEtaria <= classificacaoEtaria)
                    .ToList();

            if (filmes.Any())
                return null;
            
            var readDto = _mapper.Map<List<ReadFilmeDto>>(filmes);
            return readDto;
        }

        public ReadFilmeDto RecuperaFilmesPorId(int id)
        {
            var filme = _context.Filmes.FirstOrDefault(f => f.Id == id);
            if (filme == default)
                return null;
            
            var filmeDto = _mapper.Map<ReadFilmeDto>(filme);
            return filmeDto;
        }

        public void AtualizaFilme(int id, UpdateFilmeDto filmeDto)
        {
            var filme = _context.Filmes.FirstOrDefault(f => f.Id == id);
            if (filme == null)
                return;
            _mapper.Map(filmeDto, filme);
            _context.SaveChanges();
        }

        public void DeletaFilme(int id)
        {
            var filme = _context.Filmes.FirstOrDefault(f => f.Id == id);
            if (filme == null)
                return;
            _context.Remove(filme);
            _context.SaveChanges();
        }
    }
}