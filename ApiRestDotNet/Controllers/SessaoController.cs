using System.Threading.Tasks;
using ApiRestDotNet.Data;
using ApiRestDotNet.Data.Dtos.Sessao;
using ApiRestDotNet.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiRestDotNet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SessaoController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public SessaoController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult AdicionaSessao(CreateSessaoDto dto)
        {
            var sessao = _mapper.Map<Sessao>(dto);
            _context.Sessoes.Add(sessao);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperaSessaoPorId), new {Id = sessao.Id}, sessao);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> RecuperaSessaoPorId(int id)
        {
            var sessao = await _context.Sessoes.FirstOrDefaultAsync(s => s.Id == id);
            if (sessao == default)
                return NotFound();

            var sessaoDto = _mapper.Map<ReadSessaoDto>(sessao);
            return Ok(sessaoDto);
        }
        
    }
}