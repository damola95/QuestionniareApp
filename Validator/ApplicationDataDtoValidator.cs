using FluentValidation;
using QuestionnaireApp.DTO;

namespace QuestionnaireApp.Validator
{
    public class ApplicationDataDtoValidator : AbstractValidator<ApplicationDataDto>
    {
        public ApplicationDataDtoValidator()
        {
            RuleFor(a => a.CandidateName).NotEmpty().WithMessage("Candidate name is required.");
            RuleForEach(a => a.Answers).SetValidator(new AnswerDtoValidator());
        }
    }

    public class AnswerDtoValidator : AbstractValidator<AnswerDto>
    {
        public AnswerDtoValidator()
        {
            RuleFor(a => a.QuestionId).NotEmpty().WithMessage("Question ID is required.");
            RuleFor(a => a.Response).NotEmpty().WithMessage("Response is required.");
        }
    }
}
