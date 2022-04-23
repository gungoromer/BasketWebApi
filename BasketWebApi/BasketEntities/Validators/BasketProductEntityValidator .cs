using BasketModuleEntities.Models;
using FluentValidation;

namespace BasketModuleEntities.Validators
{
    public class BasketProductEntityValidator : AbstractValidator<BasketProduct>
    {
        public BasketProductEntityValidator()
        {
            RuleFor(p => p.Quantity).GreaterThan(0);
            RuleFor(u => u.ProductID).GreaterThan(0);
            //RuleFor(u => u.BasketID).GreaterThan(0);     
        }
    }
}