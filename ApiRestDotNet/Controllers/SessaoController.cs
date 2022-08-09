using ApiRestDotNet.Data.Dtos.Sessao;
using ApiRestDotNet.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiRestDotNet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SessaoController : ControllerBase
    {
        private SessaoService _sessaoService;

        public SessaoController(SessaoService sessaoService)
        {
            _sessaoService = sessaoService;
        }

        [HttpPost]
        public IActionResult AdicionaSessao(CreateSessaoDto dto)
        {
            var readDto = _sessaoService.AdicionaSessao(dto);
            return CreatedAtAction(nameof(RecuperaSessoesPorId), new { Id = readDto.Id }, readDto);
        }

        [HttpGet("{id}")]
        public IActionResult RecuperaSessoesPorId(int id)
        {
            var readDto = _sessaoService.RecuperaSessoesPorId(id);
            if (readDto == null) return NotFound();
            return Ok(readDto);
        }
        
    }
}