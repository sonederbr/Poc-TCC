using System.Collections.Generic;
using System.Linq;
using PocTcc.Models.Entities;

namespace PocTcc.Data
{
  public interface IRepository
  {
    Customer GetCustomerByEmail(string email);
    Customer GetCustomerById(string id);
    void AddCustomer(Customer costumer);
  }

  public class RepositoryFake : IRepository
  {
    private List<Customer> _dataRepository;
    public RepositoryFake()
    {
      _dataRepository = new List<Customer>();
    }

    public void AddCustomer(Customer costumer)
    {
      if (_dataRepository.Any(p => p.Identity.Email == costumer.Identity.Email))
      {
        return;
      }
      _dataRepository.Add(costumer);
    }

    public Customer GetCustomerByEmail(string email)
    {
      return _dataRepository.FirstOrDefault(p => p.Identity.Email == email);
    }

    public Customer GetCustomerById(string id)
    {
      return _dataRepository.FirstOrDefault(p => p.Id == id);
    }
  }
}
