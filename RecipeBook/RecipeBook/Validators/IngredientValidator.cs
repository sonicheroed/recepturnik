using FluentValidation;
using RecipeBook.Models.DTO;

namespace RecipeBook.Validators
{
    public class IngredientValidator : AbstractValidator<Ingredients>
    {
        public IngredientValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.");

            RuleFor(x => x.Calories)
                .GreaterThan(0).WithMessage("Calories must be greater than zero.");
        }
    }
}
