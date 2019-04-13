
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
  [Route("api/[controller]/[action]")]
  public class ProductController : Controller
  {
    private readonly ClaimsPrincipal _caller;
    private readonly IRepository _reposotiry;
    public ProductController(IHttpContextAccessor httpContextAccessor, IRepository repository)
    {
      _caller = httpContextAccessor.HttpContext.User;
      _reposotiry = repository;
    }

    // GET api/product/getproduct
    [HttpGet]
    public  IActionResult GetProduct()
    {
      var response = Execute();
      var content = response.Content;

      if (response.IsSuccessful)
      {
        var productDto = JsonConvert.DeserializeObject<ProductDto>(content);
        return new OkObjectResult(new
        {
          Message = "Obtendo estoque do produto!",
          Quantidade = productDto.cargo_capacity
        });
      }

      return new OkObjectResult(new
      {
        Message = "Não foi possível obter estoque do produto!",
        Quantidade = 0
      });
    }

    public IRestResponse Execute(string resource = "")
    {
      resource = resource = "https://swapi.co/api/vehicles/4/";
      IRestClient client = new RestClient(resource);
      IRestRequest request = new RestRequest(Method.GET) { RequestFormat = DataFormat.Json };
      return client.Execute(request);
    }
  }

  public class ProductDto
  {
    public string name { get; set; }
    public int cargo_capacity { get; set; }
  }
}
