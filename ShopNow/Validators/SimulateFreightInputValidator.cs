using FluentValidation;
using ShopNow.Dtos;

namespace ShopNow.Validators
{
    public class SimulateFreightInputValidator : AbstractValidator<SimulateFreightInput>
    {
        public SimulateFreightInputValidator()
        {
            RuleFor(s => s.OrderItems)
                .NotEmpty()
                .WithMessage("Para realizar o cálculo do frete é necessário pelo menos um item obrigatório!");
        }
    }
}
