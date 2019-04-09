using FluentValidation;

namespace PocTcc.ViewModels.Validations
{
  public class RegistrationViewModelValidator : AbstractValidator<RegistrationViewModel>
  {
    public RegistrationViewModelValidator()
    {
      RuleFor(vm => vm.Email).NotEmpty().WithMessage("Email deve ser informado");
      RuleFor(vm => vm.Password).NotEmpty().WithMessage("Senha  deve ser informado");
      RuleFor(vm => vm.FirstName).NotEmpty().WithMessage("Nome  deve ser informado");
      RuleFor(vm => vm.LastName).NotEmpty().WithMessage("Sobrenome  deve ser informado");
      RuleFor(vm => vm.Location).NotEmpty().WithMessage("Localização  deve ser informado");
    }
  }
}
