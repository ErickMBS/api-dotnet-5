﻿using System.Linq;
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
        private IMapper _mapper;
        private UserManager<IdentityUser<int>> _userManager;
        private EmailService _emailService;

        public CadastroService(IMapper mapper, UserManager<IdentityUser<int>> userManager, EmailService emailService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _emailService = emailService;
        }

        public Result CadastraUsuario(CreateUsuarioDto createDto)
        {
            var usuario = _mapper.Map<Usuario>(createDto);
            var usuarioIdentity = _mapper.Map<IdentityUser<int>>(usuario);
            var resultadoIdentity = _userManager
                .CreateAsync(usuarioIdentity, createDto.Password);

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