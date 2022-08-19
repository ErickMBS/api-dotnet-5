using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace ApiRestDotNet.Authorization
{
    public class IdadeMinimaHandler : AuthorizationHandler<IdadeMinimaRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, IdadeMinimaRequirement requirement
        )
        {
            if (!context.User.HasClaim(c => c.Type == ClaimTypes.DateOfBirth))
                return Task.CompletedTask;

            var dataNascimento = Convert.ToDateTime(
                context.User.FindFirst(c =>
                    c.Type == ClaimTypes.DateOfBirth
                )?.Value
            );

            var idadeObtida = DateTime.Today.Year - dataNascimento.Year;

            if (dataNascimento > DateTime.Today.AddYears(-idadeObtida))
                idadeObtida--;

            if (idadeObtida >= requirement.IdadeMInima)
                context.Succeed(requirement);
            
            return Task.CompletedTask;
        }
    }
}