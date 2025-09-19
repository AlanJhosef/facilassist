using FacilAssistApi.Command;
using FluentValidation;
using System.Text.RegularExpressions;

namespace FacilAssistApi.Validators;

public class ClienteCommandValidator : AbstractValidator<ClienteCommand>
{
    public ClienteCommandValidator()
    {
        
        RuleFor(c => c.Nome)
            .NotEmpty().WithMessage("O nome não pode ser vazio.")
            .MinimumLength(3).WithMessage("O nome deve ter no mínimo 3 caracteres.")
            .MaximumLength(100).WithMessage("O nome deve ter no máximo 100 caracteres.");

      
        RuleFor(c => c.CPF)
            .NotEmpty().WithMessage("O CPF não pode ser vazio.")
            .Length(11).WithMessage("O CPF deve ter 11 dígitos.")
            .Must(BeAValidCpf).WithMessage("O CPF não é válido.");  
 
        RuleFor(c => c.DataNascimento)
            .NotEmpty().WithMessage("A data de nascimento não pode ser vazia.")
            .Must(BeAValidAge).WithMessage("O cliente deve ter pelo menos 18 anos.");

      
        RuleFor(c => c.Sexo)
            .NotEmpty().WithMessage("O sexo não pode ser vazio.")
            .Must(s => s == "M" || s == "F").WithMessage("O sexo deve ser Masculino ou Feminino."); 
    }

   
    private bool BeAValidCpf(string cpf)
    {
        string cpfLimpo = new string(cpf.Where(char.IsDigit).ToArray());

        if (cpfLimpo.Length != 11)
        {
            return false;
        }

        if (Regex.IsMatch(cpfLimpo, @"^(\d)\1{10}$"))
        {
            return false;
        }

        
        int[] numbers = new int[11];
        for (int i = 0; i < 11; i++)
        {
            numbers[i] = int.Parse(cpfLimpo[i].ToString());
        }

        
        int sum = 0;
        for (int i = 0; i < 9; i++)
        {
            sum += numbers[i] * (10 - i);
        }
        int firstVerifierDigit = sum % 11;
        firstVerifierDigit = firstVerifierDigit < 2 ? 0 : 11 - firstVerifierDigit;

        if (firstVerifierDigit != numbers[9])
        {
            return false;
        }

        
        sum = 0;
        for (int i = 0; i < 10; i++)
        {
            sum += numbers[i] * (11 - i);
        }
        int secondVerifierDigit = sum % 11;
        secondVerifierDigit = secondVerifierDigit < 2 ? 0 : 11 - secondVerifierDigit;

        if (secondVerifierDigit != numbers[10])
        {
            return false;
        }

        return true;
         
    }

 
    private bool BeAValidAge(DateTime? dataNascimento)
    {
        if (!dataNascimento.HasValue)
        {
            return false;
        }
        var today = DateTime.Today;
        var age = today.Year - dataNascimento.Value.Year;
        if (dataNascimento.Value.Date > today.AddYears(-age))
            age--;
        return age >= 18;
    } 
}