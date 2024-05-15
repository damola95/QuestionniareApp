using Newtonsoft.Json;

namespace QuestionnaireApp.Model
{
    public  class Question
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        public string Type { get; set; }
        public string Text { get; set; }
    }

    public class ParagraphQuestion : Question { }
    public class YesNoQuestion : Question { }
    public class DropdownQuestion : Question
    {
        public List<string> Options { get; set; }
    }
    public class MultipleChoiceQuestion : Question
    {
        //public List<string> Options { get; set; }
        public List<string> Options { get; set; }
    }
    public class DateQuestion : Question { }
    public class NumberQuestion : Question { }
}

// Models/ApplicationData.cs
namespace ApplicationFormApi.Models
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
