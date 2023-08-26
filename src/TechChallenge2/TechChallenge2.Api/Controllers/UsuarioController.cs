using Microsoft.AspNetCore.Mvc;
using TechChallenge2.Identity.Models.Request;

namespace TechChallenge2.Api.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UsuarioController : ControllerBase
    {
        [HttpPost]
        public IActionResult CadastraUsuario(CadastroUsuarioRequest cadastroUsuario)
        {
            throw new NotImplementedException();
        }
    }
}
