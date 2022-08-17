using System.ComponentModel.DataAnnotations;

namespace UsuariosApi.Data.Requests
{
    public class SolicitaResetRequest
    {
        [Required] public string Email { get; set; }
    }
}