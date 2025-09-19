using FacilAssistApi.Command;
using FluentValidation;

namespace FacilAssistApi.Validators;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        
        RuleFor(c => c.Username)            
            .NotEmpty().WithMessage("Informe o usuário");

        RuleFor(c => c.Password)
            .NotEmpty().WithMessage("Informe a senha.");
    }
     
}