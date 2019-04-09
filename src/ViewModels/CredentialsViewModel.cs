using FluentValidation.Attributes;
using PocTcc.ViewModels.Validations;

namespace PocTcc.ViewModels
{
  [Validator(typeof(CredentialsViewModelValidator))]
  public class CredentialsViewModel
  {
    public string UserName { get; set; }
    public string Password { get; set; }
  }
}
