using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PocTcc.Auth;
using PocTcc.Data;
using PocTcc.Helpers;
using PocTcc.Models;
using PocTcc.ViewModels;

namespace PocTcc.Controllers
{
  [Route("api/[controller]")]
  public class AuthController : Controller
  {
    private readonly IJwtFactory _jwtFactory;
    private readonly JwtIssuerOptions _jwtOptions;
    private readonly IRepository _reposotiry;
    public AuthController(IJwtFactory jwtFactory, IOptions<JwtIssuerOptions> jwtOptions, IRepository repository)
    {
      _jwtFactory = jwtFactory;
      _jwtOptions = jwtOptions.Value;
      _reposotiry = repository;
    }
    // POST api/auth/login
    [HttpPost("login")]
    public async Task<IActionResult> Post([FromBody]CredentialsViewModel credentials)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var identity = await GetClaimsIdentity(credentials.UserName, credentials.Password);
      if (identity == null)
      {
        return BadRequest(Errors.AddErrorToModelState("login_failure", "Usuário ou senha inválido.", ModelState));
      }

      var jwt = await Tokens.GenerateJwt(identity, _jwtFactory, credentials.UserName, _jwtOptions, new JsonSerializerSettings { Formatting = Formatting.Indented });
      return new OkObjectResult(jwt);
    }
    private async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
    {
      if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
      {
        return await Task.FromResult<ClaimsIdentity>(null);
      }
      var costumer = _reposotiry.GetCustomerByEmail(userName);
      if (costumer != null && costumer.Identity.PasswordHash == password)
      {
        return await Task.FromResult(_jwtFactory.GenerateClaimsIdentity(costumer.Identity.UserName, costumer.Id));
      }
      return await Task.FromResult<ClaimsIdentity>(null);
    }
  }
}
