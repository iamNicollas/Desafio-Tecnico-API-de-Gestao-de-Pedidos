using FluentValidation;
using OrderManagement.Application.DTOs.Pedido;

namespace OrderManagement.Application.Validators.Pedido
{
    public class CreatePedidoValidator : AbstractValidator<CreatePedidoDto>
    {
        public CreatePedidoValidator()
        {
            RuleFor(x => x.ClienteId)
                .NotEmpty();

            RuleFor(x => x.Itens)
                .NotEmpty()
                .WithMessage("O pedido deve possuir pelo menos um item.");

            RuleForEach(x => x.Itens)
                .SetValidator(new CreatePedidoItemValidator());
        }
    }
}
