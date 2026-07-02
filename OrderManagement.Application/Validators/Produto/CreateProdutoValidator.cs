using FluentValidation;
using OrderManagement.Application.DTOs.Produto;

namespace OrderManagement.Application.Validators.Produto
{
    public class CreateProdutoValidator : AbstractValidator<CreateProdutoDto>
    {
        public CreateProdutoValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty()
                .MaximumLength(150);

            RuleFor(x => x.Descricao)
                .MaximumLength(500);

            RuleFor(x => x.Preco)
                .GreaterThan(0);

            RuleFor(x => x.Estoque)
                .GreaterThanOrEqualTo(0);
        }
    }
}
