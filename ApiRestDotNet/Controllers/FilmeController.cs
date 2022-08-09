using ApiRestDotNet.Data.Dtos;
using ApiRestDotNet.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiRestDotNet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {
        private readonly FilmeService _filmeService;

        public FilmeController(FilmeService filmeService)
        {
            _filmeService = filmeService;
        }

        [HttpPost]
        public IActionResult AdicionaFilme([FromBody] CreateFilmeDto filmeDto)
        {
            var readDto = _filmeService.AdicionaFilme(filmeDto);
            return CreatedAtAction(nameof(RecuperaFilmesPorId), new { readDto.Id }, readDto);
        }

        [HttpGet]
        public IActionResult RecuperaFilmes([FromQuery] int? classificacaoEtaria = null)
        {
            var readDto = _filmeService.RecuperaFilmes(classificacaoEtaria);
            if (readDto == null)
                return NotFound();
            
            return Ok(readDto);
        }

        [HttpGet("{id:int}")]
        public IActionResult RecuperaFilmesPorId(int id)
        {
            var filmeDto = _filmeService.RecuperaFilmesPorId(id);
            if (filmeDto == null)
                return NotFound();
            return Ok(filmeDto);
        }

        [HttpPut("{id:int}")]
        public IActionResult AtualizaFilme(int id, [FromBody]UpdateFilmeDto filmeDto)
        {
            _filmeService.AtualizaFilme(id, filmeDto);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeletaFilme(int id)
        {
            _filmeService.DeletaFilme(id);
            return NoContent();
        }
    }
}