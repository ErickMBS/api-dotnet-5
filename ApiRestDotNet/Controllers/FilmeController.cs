using System.Collections.Generic;
using System.Linq;
using ApiRestDotNet.Data;
using ApiRestDotNet.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiRestDotNet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {
        private readonly FilmeContext _context;

        public FilmeController(FilmeContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult AdicionaFilme([FromBody] Filme filme)
        {
            _context.Filmes.Add(filme);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperaFilmesPorId), new { Id = filme.Id }, filme);
        }

        [HttpGet]
        public IActionResult RecuperaFilmes()
        {
            return Ok(_context.Filmes);
        }

        [HttpGet("{id:int}")]
        public IActionResult RecuperaFilmesPorId(int id)
        {
            var filme = _context.Filmes.FirstOrDefault(f => f.Id == id);
            return filme == default 
                ? NotFound()
                : Ok(filme);
        }

        [HttpPut("{id:int}")]
        public IActionResult AtualizaFilme(int id, [FromBody]Filme filme)
        {
            var filmeOriginal = _context.Filmes.FirstOrDefault(f => f.Id == id);

            if (filmeOriginal == null)
                return NotFound();

            filmeOriginal.Titulo = filme.Titulo;
            filmeOriginal.Genero = filme.Genero;
            filmeOriginal.Duracao = filme.Duracao;
            filmeOriginal.Diretor = filme.Diretor;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeletaFilme(int id)
        {
            var filme = _context.Filmes.FirstOrDefault(f => f.Id == id);

            if (filme == null)
                return NotFound();
            
            _context.Remove(filme);
            _context.SaveChanges();
            return NoContent();
        }
    }
}