using Microsoft.AspNetCore.Authorization;

namespace ApiRestDotNet.Authorization
{
    public class IdadeMinimaRequirement : IAuthorizationRequirement
    {
        public int IdadeMInima { get; set; }

        public IdadeMinimaRequirement(int idadeMinima)
        {
            IdadeMInima = idadeMinima;
        }
    }
}