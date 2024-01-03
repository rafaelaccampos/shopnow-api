using FluentValidation;
using ShopNow.Dtos;

namespace ShopNow.Validators
{
    public class OrderItemInputValidator : AbstractValidator<OrderItemInput>
    {
        public OrderItemInputValidator()
        {
            RuleFor(o => o.IdItem)
                .NotEmpty()
                .WithMessage("O código do item é obrigatório!");
            RuleFor(o => o.Count)
                .NotEmpty()
                .WithMessage("A quantidade de itens é obrigatório!");
        }
    }
}
