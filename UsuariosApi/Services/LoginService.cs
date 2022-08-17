using System.Linq;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using UsuariosApi.Data.Requests;
using UsuariosApi.Models;

namespace UsuariosApi.Services
{
    public class LoginService
    {
        private SignInManager<IdentityUser<int>> _signInManager;
        private TokenService _tokenService;

        public LoginService(SignInManager<IdentityUser<int>> signInManager,
            TokenService tokenService)
        {
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public Result LogaUsuario(LoginRequest request)
        {
            var resultadoIdentity = _signInManager
                .PasswordSignInAsync(request.Username, request.Password, false, false);
            if (!resultadoIdentity.Result.Succeeded) 
                return Result.Fail("Login falhou");
            
            var identityUser = _signInManager
                .UserManager
                .Users
                .FirstOrDefault(usuario => 
                    usuario.NormalizedUserName == request.Username.ToUpper());
            var token = _tokenService.CreateToken(identityUser);
            return Result.Ok().WithSuccess(token.Value);
        }

        public Result SolicitaResetSenhaUsuario(SolicitaResetRequest request)
        {
            var identityUser = RecuperaUsuarioPorEmail(request.Email);
            if (identityUser == null) 
                return Result.Fail("Falha ao solicitar redefinição");
            
            var codigoDeRecuperacao = _signInManager
                .UserManager
                .GeneratePasswordResetTokenAsync(identityUser)
                .Result;
            return Result.Ok().WithSuccess(codigoDeRecuperacao);

        }

        public Result ResetaSenhaUsuario(EfetuaResetRequest request)
        {
            var identityUser = RecuperaUsuarioPorEmail(request.Email);
            
            var resultadoIdentity = _signInManager
                .UserManager
                .ResetPasswordAsync(identityUser, request.Token, request.Password)
                .Result;
            
            return resultadoIdentity.Succeeded 
                ? Result.Ok().WithSuccess("Senha redefinida com sucesso!") 
                : Result.Fail("Falha ao redefinir a senha!");
        }

        private IdentityUser<int> RecuperaUsuarioPorEmail(string email)
        {
            var identityUser = _signInManager
                .UserManager
                .Users
                .FirstOrDefault(u => u.NormalizedEmail == email.ToUpper());
            return identityUser;
        }
    }
}