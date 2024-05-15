using QuestionnaireApp.DTO;
using QuestionnaireApp.Model;

namespace QuestionnaireApp.Interface
{
    public interface IProgramApplicationRepo
    {
        Task<Programs> AddQuestionAsync(Programs question);
        Task<IEnumerable<Programs>> GetQuestionsAsync();
    }

   
}
