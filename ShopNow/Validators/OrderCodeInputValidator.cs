using FluentValidation;
using ShopNow.Dtos;

namespace ShopNow.Validators
{
    public class OrderCodeInputValidator : AbstractValidator<OrderCodeInput>
    {
        public OrderCodeInputValidator()
        {
            RuleFor(o => o.OrderCode)
                .NotEmpty()
                .WithMessage("O código do pedido é obrigatório");
        }
    }
}
