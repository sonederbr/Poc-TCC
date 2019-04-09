using FluentValidation;

namespace PocTcc.ViewModels.Validations
{
  public class CredentialsViewModelValidator : AbstractValidator<CredentialsViewModel>
  {
    public CredentialsViewModelValidator()
    {
      RuleFor(vm => vm.UserName).NotEmpty().WithMessage("Username deve ser informado");
      RuleFor(vm => vm.Password).NotEmpty().WithMessage("Senha deve ser informado");
      RuleFor(vm => vm.Password).Length(6, 12).WithMessage("Senha precisa ser entre 6 e 12 caracteres");
    }
  }
}
