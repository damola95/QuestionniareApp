

using Newtonsoft.Json;

namespace QuestionnaireApp.Model
{
    public class ProgramApplicationModel
    {
       
    }

    public class Programs
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; } =  string.Empty;
        public string Title { get; set; }
        public string Description { get; set; }
        public PersonaInfo personaInfo { get; set; }
        public Question question { get; set; }
    }

    public class PersonaInfo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Nationality { get; set; }
        public string currentResidence { get; set; }
        public string IDNumber { get; set; }
        public DateTime DoB { get; set; }
        public Char Gender { get; set; }
    }
}
