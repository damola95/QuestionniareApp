//using QuestionnaireApp.DTOs;
//using QuestionnaireApp.Models;
using QuestionnaireApp.Interface;
using QuestionnaireApp.Model;
using QuestionnaireApp.DTO;
using QuestionnaireApp.Repo;

namespace QuestionnaireApp.Service
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;

        public QuestionService(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }


        public async Task<Question> CreateQuestionAsync(QuestionDto questionDto)
        {
            var question = CreateQuestionFromDto(questionDto);
            return await _questionRepository.AddQuestionAsync(question);
        }

        public async Task<Question> UpdateQuestionAsync(string id, QuestionDto questionDto)
        {
            var question = CreateQuestionFromDto(questionDto);
            question.Id = id;
            return await _questionRepository.UpdateQuestionAsync(question);
        }

        public async Task<IEnumerable<Question>> GetQuestionsAsync()
        {
            return await _questionRepository.GetQuestionsAsync();
        }

        public async Task<ApplicationData> SubmitApplicationAsync(ApplicationDataDto applicationDataDto)
        {
            var applicationData = new ApplicationData
            {
                Id = Guid.NewGuid().ToString(),
                CandidateName = applicationDataDto.CandidateName,
                Answers = applicationDataDto.Answers.Select(a => new Answer
                {
                    QuestionId = a.QuestionId,
                    Response = a.Response
                }).ToList()
            };

            return await _questionRepository.AddApplicationDataAsync(applicationData);
        }

        private Question CreateQuestionFromDto(QuestionDto questionDto)
        {
            return questionDto.Type switch
            {
                "Paragraph" => new Model.ParagraphQuestion { Id = questionDto.Id, Type = questionDto.Type, Text = questionDto.Text },
                "YesNo" => new YesNoQuestion { Id = questionDto.Id, Type = questionDto.Type, Text = questionDto.Text },
                "Dropdown" => new DropdownQuestion { Id = questionDto.Id, Type = questionDto.Type, Text = questionDto.Text, Options = questionDto.Options },
                "MultipleChoice" => new MultipleChoiceQuestion { Id = questionDto.Id, Type = questionDto.Type, Text = questionDto.Text, Options = questionDto.Options },
                "Date" => new DateQuestion { Id = questionDto.Id, Type = questionDto.Type, Text = questionDto.Text },
                "Number" => new NumberQuestion { Id = questionDto.Id, Type = questionDto.Type, Text = questionDto.Text },
                _ => throw new ArgumentException("Invalid question type", nameof(questionDto.Type))
            };
        }

        private Question MapQuestionDtoToModel(QuestionDto questionDto)
        {
            switch (questionDto.Type)
            {
                case "Paragraph":
                    return new ParagraphQuestion
                    {
                        Id = questionDto.Id,
                        Type = questionDto.Type,
                        Text = questionDto.Text
                    };
                case "YesNo":
                    return new YesNoQuestion
                    {
                        Id = questionDto.Id,
                        Type = questionDto.Type,
                        Text = questionDto.Text
                    };
                case "Dropdown":
                    return new DropdownQuestion
                    {
                        Id = questionDto.Id,
                        Type = questionDto.Type,
                        Text = questionDto.Text,
                        Options = questionDto.Options
                    };
                case "MultipleChoice":
                    return new MultipleChoiceQuestion
                    {
                        Id = questionDto.Id,
                        Type = questionDto.Type,
                        Text = questionDto.Text,
                        Options = questionDto.Options
                    };
                case "Date":
                    return new DateQuestion
                    {
                        Id = questionDto.Id,
                        Type = questionDto.Type,
                        Text = questionDto.Text
                    };
                case "Number":
                    return new NumberQuestion
                    {
                        Id = questionDto.Id,
                        Type = questionDto.Type,
                        Text = questionDto.Text
                    };
                default:
                    throw new ArgumentException("Invalid question type", nameof(questionDto.Type));
            }
        }
    }

}
