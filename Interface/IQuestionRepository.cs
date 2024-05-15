using QuestionnaireApp.Model;

namespace QuestionnaireApp.Interface
{
    public interface IQuestionRepository
    {
     
            Task<Question> AddQuestionAsync(Question question);
            Task<Question> UpdateQuestionAsync(Question question);
            Task<IEnumerable<Question>> GetQuestionsAsync();
            Task<ApplicationData> AddApplicationDataAsync(ApplicationData applicationData);
        
    }
}
