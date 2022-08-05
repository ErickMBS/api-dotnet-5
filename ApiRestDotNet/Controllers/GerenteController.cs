using System.Linq;
using ApiRestDotNet.Data;
using ApiRestDotNet.Data.Dtos.Gerente;
using ApiRestDotNet.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ApiRestDotNet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GerenteController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public GerenteController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult AdicionaGerente(CreateGerenteDto dto)
        {
            var gerente = _mapper.Map<Gerente>(dto);
            _context.Gerentes.Add(gerente);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperarGerentePorId), new {Id = gerente.Id}, gerente);
        }

        [HttpGet("{id:int}")]
        public IActionResult RecuperarGerentePorId(int id)
        {
            var gerente = _context.Gerentes.FirstOrDefault(gerente => gerente.Id == id);
            if (gerente == null) 
                return NotFound();
            
            var gerenteDto = _mapper.Map<ReadGerenteDto>(gerente);
            return Ok(gerenteDto);
        }
        
        [HttpDelete("{id:int}")]
        public IActionResult DeletaGerente(int id)
        {
            var gerente = _context.Gerentes.FirstOrDefault(g => g.Id == id);

            if (gerente == null)
                return NotFound();
            
            _context.Remove(gerente);
            _context.SaveChanges();
            return NoContent();
        }
    }
}