using TechChallenge2.Identity.Models.Request;
using TechChallenge2.Identity.Models.Response;

namespace TechChallenge2.Identity.Interfaces.Services;

public interface IIdentityService
{
    Task<CadastroUsuarioResponse> CadastrarUsuario(CadastroUsuarioRequest cadastroUsuario);
}