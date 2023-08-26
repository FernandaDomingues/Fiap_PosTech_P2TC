using TechChallenge2.Identity.Configurations;
using TechChallenge2.Identity.Interfaces.Services;
using TechChallenge2.Identity.Models.Request;
using TechChallenge2.Identity.Models.Response;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.VisualBasic;
using System.Net.Mail;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace TechChallenge2.Identity.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtConfig _jwtOptions;

        public IdentityService(SignInManager<IdentityUser> signInManager,
                               UserManager<IdentityUser> userManager,
                               IOptions<JwtConfig> jwtOptions)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtOptions = jwtOptions.Value;
        }

        public async Task<CadastroUsuarioResponse> CadastrarUsuario(CadastroUsuarioRequest cadastroUsuario)
        {
            var identityUser = new IdentityUser
            {
                UserName = cadastroUsuario.Name,
                Email = cadastroUsuario.Email,
                NormalizedEmail = cadastroUsuario.Email
            };

            var result = await _userManager.CreateAsync(identityUser, cadastroUsuario.Password);

            var usuarioCadastroResponse = new CadastroUsuarioResponse(result.Succeeded);

            if (!result.Succeeded && result.Errors.Any())
            {
                string descricaoErro = await TratarErroResponse(null, identityUser.Email, result);
               // usuarioCadastroResponse.AdicionarErros(new List<string> { descricaoAmigavelErro });
            }
            else if (result.Succeeded)
            {
                usuarioCadastroResponse.Message = $"Link de confirmação do cadastro enviado para o email: {cadastroUsuario.Email}";
            }

            return usuarioCadastroResponse;
        }
        private async Task<string> TratarErroResponse(SignInResult result, string email, IdentityResult identityResult)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (identityResult != null && identityResult.Errors.Any())
                return identityResult.Errors.First().Description.ToString();
            else if (user == null)
                return "Essa conta está bloqueada";
            else if (!await _signInManager.UserManager.IsEmailConfirmedAsync(user))
                return "Confirmação por email pendente.";
            else if (result != null && result.IsLockedOut)
                return "Essa conta está bloqueada";
            else if (result != null && result.IsNotAllowed)
                return "Essa conta não tem permissão para fazer login";
            else if (result != null && result.RequiresTwoFactor)
                return "É necessário confirmar o login no seu segundo fator de autenticação";
            else
                return "Usuário e/ou senha inválido!";
        }

        //private async Task<LoginUsuarioResponse> GerarCredenciais(string email)
        //{
        //    var user = await _userManager.FindByEmailAsync(email);
        //    var accessTokenClaims = await ObterClaims(user, adicionarClaimsUsuario: true);
        //    var refreshTokenClaims = await ObterClaims(user, adicionarClaimsUsuario: false);

        //    var dataExpiracaoAccessToken = DateTime.Now.AddSeconds(_jwtOptions.AccessTokenExpiration);
        //    var dataExpiracaoRefreshToken = DateTime.Now.AddSeconds(_jwtOptions.RefreshTokenExpiration);

        //    var accessToken = GerarToken(accessTokenClaims, dataExpiracaoAccessToken);
        //    var refreshToken = GerarToken(refreshTokenClaims, dataExpiracaoRefreshToken);

        //    return new LoginUsuarioResponse
        //    (
        //        sucesso: true,
        //        accessToken: accessToken,
        //        refreshToken: refreshToken
        //    );
        //}

        private string GerarToken(IEnumerable<Claim> claims, DateTime dataExpiracao)
        {
            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                notBefore: DateTime.Now,
                expires: dataExpiracao,
                signingCredentials: _jwtOptions.SigningCredentials);

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
