
using System;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using UsuariosApi.Models;

namespace UsuariosApi.Services
{
    public class EmailService
    {
        private IConfiguration _configuration;
        
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public void EnviarEmail(string[] destinatarios, string assunto, int usuarioId, string code)
        {
            Mensagem mensagem = new Mensagem(destinatarios, assunto, usuarioId, code);
            var mensagemDeEmail = CriaCorpoDoEmail(mensagem);
            Enviar(mensagemDeEmail);
        }

        private void Enviar(MimeMessage mensagemDeEmail)
        {
            using (var cliente = new SmtpClient())
            {
                try
                {
                    cliente.Connect(
                        _configuration.GetValue<string>("EmailSettings:SmtpServer"),
                        _configuration.GetValue<int>("EmailSettings:Port"),
                        true
                    );
                    cliente.AuthenticationMechanisms.Remove("XOUATH2");
                    cliente.Authenticate(
                        _configuration.GetValue<string>("EmailSettings:From"),
                        _configuration.GetValue<string>("EmailSettings:Password")
                    );
                    cliente.Send(mensagemDeEmail);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                finally
                {
                    cliente.Disconnect(true);
                    cliente.Dispose();
                }
            }
        }

        private MimeMessage CriaCorpoDoEmail(Mensagem mensagem)
        {
            var mensagemDeEmail = new MimeMessage();
            mensagemDeEmail.From.Add(new MailboxAddress(
                "FilmesApiAuth",
                _configuration.GetValue<string>("EmailSettings:From")
            ));
            mensagemDeEmail.To.AddRange(mensagem.Destinatarios);
            mensagemDeEmail.Subject = mensagem.Assunto;
            mensagemDeEmail.Body = new TextPart(TextFormat.Text)
            {
                Text = mensagem.Conteudo
            };

            return mensagemDeEmail;
        }
    }
}