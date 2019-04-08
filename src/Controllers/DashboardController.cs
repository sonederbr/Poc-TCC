
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using PocTcc.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PocTcc.Data;
using System;

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
      // retrieve the user info
      //HttpContext.User
      var userId = _caller.Claims.Single(c => c.Type == "id");
      var customer = _reposotiry.GetCustomerById(userId.Value);
      return new OkObjectResult(new
      {
        Message = "This is secure API and user data!",
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
