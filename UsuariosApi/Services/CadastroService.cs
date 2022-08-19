using System.Linq;
using System.Web;
using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using UsuariosApi.Data.Dtos;
using UsuariosApi.Data.Requests;
using UsuariosApi.Models;

namespace UsuariosApi.Services
{
    public class CadastroService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<CustomIdentityUser> _userManager;
        private readonly EmailService _emailService;

        public CadastroService(
            IMapper mapper, 
            UserManager<CustomIdentityUser> userManager, 
            EmailService emailService
        )
        {
            _mapper = mapper;
            _userManager = userManager;
            _emailService = emailService;
        }

        public Result CadastraUsuario(CreateUsuarioDto createDto)
        {
            var usuario = _mapper.Map<Usuario>(createDto);
            var usuarioIdentity = _mapper.Map<CustomIdentityUser>(usuario);
            var resultadoIdentity = _userManager
                .CreateAsync(usuarioIdentity, createDto.Password);

            _userManager.AddToRoleAsync(usuarioIdentity, "regular");

            if (!resultadoIdentity.Result.Succeeded)
                return Result.Fail("Falha ao cadastrar usuário");
            
            var code = _userManager.GenerateEmailConfirmationTokenAsync(usuarioIdentity).Result;
            var encodedCode = HttpUtility.UrlEncode(code);
                
            _emailService.EnviarEmail(
                new[] { usuarioIdentity.Email }, 
                "Link de Ativação", usuarioIdentity.Id, encodedCode
            );
            return Result.Ok().WithSuccess(code);

        }

        public Result AtivaContaUsuario(AtivaContaRequest request)
        {
            var identityUser = _userManager.Users.FirstOrDefault(u => u.Id == request.UsuarioId);
            var identityResult = _userManager
                .ConfirmEmailAsync(identityUser, request.CodigoDeAtivacao)
                .Result;
            
            return identityResult.Succeeded 
                ? Result.Ok() 
                : Result.Fail("Falha na ativação do usuário");
        }
    }
}