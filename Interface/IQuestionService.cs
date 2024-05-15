using QuestionnaireApp.DTO;
using QuestionnaireApp.DTO;
using QuestionnaireApp.Model;

namespace QuestionnaireApp.Interface
{
    public interface IQuestionService
    {
        Task<Question> CreateQuestionAsync(QuestionDto questionDto);
        Task<Question> UpdateQuestionAsync(string id, QuestionDto questionDto);
        Task<IEnumerable<Question>> GetQuestionsAsync();
        Task<ApplicationData> SubmitApplicationAsync(ApplicationDataDto applicationDataDto);

       
    }
}
