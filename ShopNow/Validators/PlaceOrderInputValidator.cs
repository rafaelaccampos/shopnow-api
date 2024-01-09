using FluentValidation;
using ShopNow.Dtos;

namespace ShopNow.Validators
{
    public class PlaceOrderInputValidator : AbstractValidator<PlaceOrderInput>
    {
        public PlaceOrderInputValidator()
        {
            RuleFor(p => p.Cpf)
                .NotEmpty()
                .WithMessage("O cpf é obrigatório!")
                .MinimumLength(11)
                .WithMessage("O cpf deve ter no mínimo 11 dígitos!");
            RuleFor(p => p.OrderItems)
                .NotEmpty()
                .WithMessage("O pedido precisa ter pelo menos um pedido!");
        }
    }
}
