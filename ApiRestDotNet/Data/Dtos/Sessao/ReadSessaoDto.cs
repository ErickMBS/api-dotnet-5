using System;
using ApiRestDotNet.Models;

namespace ApiRestDotNet.Data.Dtos.Sessao
{
    public class ReadSessaoDto
    {
        public int Id { get; set; }
        public Filme Filme { get; set; }
        public Models.Cinema Cinema { get; set; }
        public DateTime HorarioDeEncerramento { get; set; }
    }
}