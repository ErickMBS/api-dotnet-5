using System.Collections.Generic;
using System.Linq;
using ApiRestDotNet.Data;
using ApiRestDotNet.Data.Dtos.Cinema;
using ApiRestDotNet.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ApiRestDotNet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CinemaController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CinemaController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult AdicionaCinema([FromBody] CreateCinemaDto cinemaDto)
        {
            var cinema = _mapper.Map<Cinema>(cinemaDto);
            _context.Cinemas.Add(cinema);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperaCinemasPorId), new { Id = cinema.Id }, cinema);
        }

        [HttpGet]
        public IActionResult RecuperaCinemas([FromQuery] string nomeDoFilme)
        {
            var cinemas = _context.Cinemas.ToList();
            if (!cinemas.Any())
                return NotFound();

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
            return Ok(readDto);
        }

        [HttpGet("{id:int}")]
        public IActionResult RecuperaCinemasPorId(int id)
        {
            var cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
            if (cinema == null) 
                return NotFound();
            var cinemaDto = _mapper.Map<ReadCinemaDto>(cinema);
            return Ok(cinemaDto);
        }

        [HttpPut("{id:int}")]
        public IActionResult AtualizaCinema(int id, [FromBody] UpdateCinemaDto cinemaDto)
        {
            var cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
            if(cinema == null)
            {
                return NotFound();
            }
            _mapper.Map(cinemaDto, cinema);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeletaCinema(int id)
        {
            var cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
            if (cinema == null)
            {
                return NotFound();
            }
            _context.Remove(cinema);
            _context.SaveChanges();
            return NoContent();
        }

    }
}
