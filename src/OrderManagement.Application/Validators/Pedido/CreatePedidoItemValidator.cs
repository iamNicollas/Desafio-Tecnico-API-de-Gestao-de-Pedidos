using FluentValidation;
using OrderManagement.Application.DTOs.Pedido;

namespace OrderManagement.Application.Validators.Pedido
{
    public class CreatePedidoItemValidator : AbstractValidator<CreatePedidoItemDto>
    {
        public CreatePedidoItemValidator()
        {
            RuleFor(x => x.ProdutoId)
                .NotEmpty();

            RuleFor(x => x.Quantidade)
                .GreaterThan(0);
        }
    }
}
