namespace QuestionnaireApp.Model
{
    public class ApplicationData
    {
        public string Id { get; set; }
        public string CandidateName { get; set; }
        public List<Answer> Answers { get; set; }
    }

    public class Answer
    {
        public string QuestionId { get; set; }
        public string Response { get; set; }
    }
}
