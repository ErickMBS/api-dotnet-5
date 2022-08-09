using System.Collections.Generic;
using System.Linq;
using ApiRestDotNet.Data;
using ApiRestDotNet.Data.Dtos;
using ApiRestDotNet.Data.Dtos.Cinema;
using ApiRestDotNet.Models;
using AutoMapper;
using FluentResults;

namespace ApiRestDotNet.Services
{
    public class CinemaService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CinemaService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ReadCinemaDto AdicionaCinema(CreateCinemaDto cinemaDto)
        {
            var cinema = _mapper.Map<Cinema>(cinemaDto);
            _context.Cinemas.Add(cinema);
            _context.SaveChanges();
            return _mapper.Map<ReadCinemaDto>(cinema);
        }

        public List<ReadCinemaDto> RecuperaCinemas(string nomeDoFilme)
        {
            var cinemas = _context.Cinemas.ToList();
            if (!cinemas.Any())
                return null;

            if (!string.IsNullOrWhiteSpace(nomeDoFilme))
            {
                var query = from cinema in cinemas 
                    where cinema.Sessoes.Any(sessao => 
                        sessao.Filme.Titulo == nomeDoFilme
                    )
                    select cinema;
                cinemas = query.ToList();
            }

            var readDto = _mapper.Map<List<ReadCinemaDto>>(cinemas);
            return readDto;
        }

        public ReadCinemaDto RecuperaCinemaPorId(int id)
        {
            var cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
            if (cinema == null) 
                return null;
            var cinemaDto = _mapper.Map<ReadCinemaDto>(cinema);
            return cinemaDto;
        }

        public Result AtualizaCinema(int id, UpdateCinemaDto cinemaDto)
        {
            var cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
            if (cinema == null)
                return Result.Fail("Cinema não encontrado");
            _mapper.Map(cinemaDto, cinema);
            _context.SaveChanges();
            return Result.Ok();
        }

        public Result DeletaCinema(int id)
        {
            var cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
            if (cinema == null)
                return Result.Fail("Cinema não encontrado");    
            _context.Remove(cinema);
            _context.SaveChanges();
            return Result.Ok();
        }
    }
}