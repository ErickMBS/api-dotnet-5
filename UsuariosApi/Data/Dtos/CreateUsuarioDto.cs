using System;
using System.ComponentModel.DataAnnotations;

namespace UsuariosApi.Data.Dtos
{
    public class CreateUsuarioDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType("password")]
        public string Password { get; set; }
        [DataType("password")]
        [Required]
        [Compare("Password")]
        public string RePassword { get; set; }
        [Required]
        public DateTime DataNascimento { get; set; }
    }
}