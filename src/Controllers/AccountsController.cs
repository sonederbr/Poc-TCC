using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PocTcc.Data;
using PocTcc.Helpers;
using PocTcc.Models.Entities;
using PocTcc.ViewModels;

namespace PocTcc.Controllers
{
  [Route("api/[controller]")]
  public class AccountsController : Controller
  {
    private readonly IMapper _mapper;
    private readonly IRepository _reposotiry;
    public AccountsController(IMapper mapper, IRepository repository)
    {
      _mapper = mapper;
      _reposotiry = repository;
    }

    // POST api/accounts
    [HttpPost]
    public async Task<IActionResult> Post([FromBody]RegistrationViewModel model)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var userIdentity = _mapper.Map<AppUser>(model);
      userIdentity.PasswordHash = model.Password;

      var result = _reposotiry.GetCustomerByEmail(model.Email);
      if (result != null)
      {
        return new BadRequestObjectResult(Errors.AddErrorToModelState("1001", "Usuário já cadastrado com este email", ModelState));
      }

      _reposotiry.AddCustomer(new Customer(userIdentity.Id, userIdentity, model.Location, "", ""));
      return new OkObjectResult("Cadastro efetuado com sucesso.");
    }
  }
}
