using ApiRestDotNet.Data.Dtos;
using ApiRestDotNet.Services;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "admin")]
        public IActionResult AdicionaFilme([FromBody] CreateFilmeDto filmeDto)
        {
            var readDto = _filmeService.AdicionaFilme(filmeDto);
            return CreatedAtAction(nameof(RecuperaFilmesPorId), new { readDto.Id }, readDto);
        }

        [HttpGet]
        [Authorize(Roles = "admin, regular", Policy = "IdadeMinima")]
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
            var resultado = _filmeService.AtualizaFilme(id, filmeDto);
            if (resultado.IsFailed)
                return NotFound();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeletaFilme(int id)
        {
            var resultado = _filmeService.DeletaFilme(id);
            if (resultado.IsFailed)
                return NotFound();
            return NoContent();
        }
    }
}