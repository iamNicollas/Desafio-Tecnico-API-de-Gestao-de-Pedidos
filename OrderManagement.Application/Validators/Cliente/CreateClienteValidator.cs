using FluentValidation;
using OrderManagement.Application.DTOs.Cliente;

namespace OrderManagement.Application.Validators.Cliente
{
    public class CreateClienteValidator :AbstractValidator<CreateClienteDto>
    {
        public CreateClienteValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O nome é obrigatório.")
                .MaximumLength(150);

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("O e-mail é obrigatório.")
                .EmailAddress().WithMessage("E-mail inválido.");

            RuleFor(x => x.Documento)
                .NotEmpty().WithMessage("O documento é obrigatório.");
        }
    }
}
