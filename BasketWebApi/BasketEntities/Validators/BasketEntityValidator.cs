using BasketModuleEntities.Models;
using FluentValidation;

namespace BasketModuleEntities.Validators
{
    public class BasketEntityValidator : AbstractValidator<Basket>
    {
        public BasketEntityValidator()
        {           
            RuleFor(p => p.CreateDate).NotNull();
            RuleFor(u => u.UserID).GreaterThan(0);            
        }
    }
}