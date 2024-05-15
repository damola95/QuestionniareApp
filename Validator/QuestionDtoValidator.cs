using FluentValidation;
using QuestionnaireApp.DTO;

namespace QuestionnaireApp.Validator
{
    public class QuestionDtoValidator : AbstractValidator<QuestionDto>
    {
        public QuestionDtoValidator()
        {
            RuleFor(q => q.Type).NotEmpty().WithMessage("Question type is required.");
            RuleFor(q => q.Text).NotEmpty().WithMessage("Question text is required.");

            RuleFor(q => q.Options).NotEmpty().When(q => q.Type == "Dropdown" || q.Type == "MultipleChoice")
                .WithMessage("Options are required for Dropdown and MultipleChoice questions.");
        }
    }
}
