using System;
using System.Collections.Generic;
using ApiRestDotNet.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiRestDotNet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : Controller
    {
        private static List<Filme> filmes = new List<Filme>();
        
        [HttpPost]
        public ActionResult AdicionaFilme(Filme filme)
        {
            filmes.Add(filme);
            
            return Created("", filme);
        }
    }
}