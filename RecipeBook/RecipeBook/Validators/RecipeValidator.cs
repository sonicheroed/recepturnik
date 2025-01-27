using FluentValidation;
using RecipeBook.Models.DTO;

namespace RecipeBook.Validators
{
    public class RecipeValidator : AbstractValidator<Recipe>
    {
        public RecipeValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.");

            RuleFor(x => x.Ingredients)
                .NotEmpty().WithMessage("At least one ingredient is required.")
                .Must(ingredients => ingredients.Count > 0).WithMessage("At least one ingredient must be provided.");
        }
    }
}