namespace QuestionnaireApp.DTO
{
    public class ApplicationDataDto
    {
        public string CandidateName { get; set; }
        public List<AnswerDto> Answers { get; set; }
    }

    public class AnswerDto
    {
        public string QuestionId { get; set; }
        public string Response { get; set; }
    }
}
