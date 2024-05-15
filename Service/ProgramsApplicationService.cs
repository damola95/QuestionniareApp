using QuestionnaireApp.DTO;
using QuestionnaireApp.Interface;
using QuestionnaireApp.Model;

namespace QuestionnaireApp.Service
{
    public class ProgramsApplicationService : IProgramApplicationService
    {
        private readonly IProgramApplicationRepo _questionRepository;

        public ProgramsApplicationService(IProgramApplicationRepo questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public async Task<Programs> AddPorgramApplicationAsync(Programs programs)
        {
            
            return await _questionRepository.AddQuestionAsync(programs);
        }

       
        public async Task<IEnumerable<Programs>> GetQuestionsAsync()
        {
            return await _questionRepository.GetQuestionsAsync();
        }
    }
}
