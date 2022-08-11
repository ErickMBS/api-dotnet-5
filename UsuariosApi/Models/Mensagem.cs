using System.Collections.Generic;
using System.Linq;
using MimeKit;

namespace UsuariosApi.Models
{
    public class Mensagem
    {
        public List<MailboxAddress> Destinatarios { get; set; }
        public string Assunto { get; set; }
        public string Conteudo { get; set; }

        public Mensagem(IEnumerable<string> destinatarios, string assunto, int usuarioId, string codigo)
        {
            Destinatarios = new List<MailboxAddress>();
            Destinatarios.AddRange(destinatarios.Select(d => new MailboxAddress(d, d)));
            Assunto = assunto;
            Conteudo = $"http://localhost:5000/ativa?UsuarioId={usuarioId}&CodigoDeAtivacao={codigo}";
        }
    }
}