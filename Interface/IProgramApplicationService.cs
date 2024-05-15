using QuestionnaireApp.Model;
using  QuestionnaireApp.Model;

namespace QuestionnaireApp.Interface
{
    public interface IProgramApplicationService
    {
        Task<Programs> AddPorgramApplicationAsync(Programs question);
        Task<IEnumerable<Programs>> GetQuestionsAsync();
    }
}
