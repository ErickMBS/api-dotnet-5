using System.ComponentModel.DataAnnotations;

namespace ApiRestDotNet.Data.Dtos.Cinema
{
    public class ReadCinemaDto
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo de nome é obrigatório")]
        public string Nome { get; set; }
        public object Endereco { get; set; }
    }
}