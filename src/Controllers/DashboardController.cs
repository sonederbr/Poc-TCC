
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PocTcc.Data;
using RestSharp;

namespace PocTcc.Controllers
{
  [Authorize(Policy = "ApiUser")]
  [Route("api/[controller]/[action]")]
  public class DashboardController : Controller
  {
    private readonly ClaimsPrincipal _caller;
    private readonly IRepository _reposotiry;
    public DashboardController(IHttpContextAccessor httpContextAccessor, IRepository repository)
    {
      _caller = httpContextAccessor.HttpContext.User;
      _reposotiry = repository;
    }
    // GET api/dashboard/home
    [HttpGet]
    public async Task<IActionResult> Home()
    {
      //HttpContext.User
      var userId = _caller.Claims.Single(c => c.Type == "id");
      var customer = _reposotiry.GetCustomerById(userId.Value);
      return new OkObjectResult(new
      {
        Message = "Esta é uma API segura e dados do usuário!",
        customer.Identity.FirstName,
        customer.Identity.LastName,
        customer.Identity.PictureUrl,
        customer.Location,
        customer.Locale,
        customer.Gender
      });
    }
  }
}
