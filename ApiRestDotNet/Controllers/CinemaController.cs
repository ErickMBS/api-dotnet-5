using System.Collections.Generic;
using System.Linq;
using ApiRestDotNet.Data;
using ApiRestDotNet.Data.Dtos.Cinema;
using ApiRestDotNet.Models;
using ApiRestDotNet.Services;
using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace ApiRestDotNet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CinemaController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly CinemaService _cinemaService;

        public CinemaController(AppDbContext context, IMapper mapper, CinemaService cinemaService)
        {
            _context = context;
            _mapper = mapper;
            _cinemaService = cinemaService;
        }

        [HttpPost]
        public IActionResult AdicionaCinema([FromBody] CreateCinemaDto cinemaDto)
        {
            var dto = _cinemaService.AdicionaCinema(cinemaDto);
            return CreatedAtAction(nameof(RecuperaCinemasPorId), new { Id = dto.Id }, dto);
        }

        [HttpGet]
        public IActionResult RecuperaCinemas([FromQuery] string nomeDoFilme)
        {
            var readDto = _cinemaService.RecuperaCinemas(nomeDoFilme);
            if (readDto == null)
                return NotFound();
            return Ok(readDto);
        }

        [HttpGet("{id:int}")]
        public IActionResult RecuperaCinemasPorId(int id)
        {
            var dto = _cinemaService.RecuperaCinemaPorId(id);
            if (dto == null)
                return NotFound();
            return Ok(dto);
        }

        [HttpPut("{id:int}")]
        public IActionResult AtualizaCinema(int id, [FromBody] UpdateCinemaDto cinemaDto)
        {
            var resultado = _cinemaService.AtualizaCinema(id, cinemaDto);
            if (resultado.IsFailed)
                return NotFound();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeletaCinema(int id)
        {
            var resultado = _cinemaService.DeletaCinema(id);
            if (resultado.IsFailed)
                return NotFound();
            return NoContent();
        }

    }
}
