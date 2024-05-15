using Microsoft.Azure.Cosmos;
using QuestionnaireApp.Constants;
using QuestionnaireApp.Interface;
using QuestionnaireApp.Model;
using System.Net;
using static QuestionnaireApp.Model.ProgramApplicationModel;

namespace QuestionnaireApp.Repo
{
    public class ApplicationRepo : IProgramApplicationRepo
    {
        private Container _container;
        private Database database;

        public ApplicationRepo(CosmosClient cosmosClient, Database dataBaseQuestions, Microsoft.Azure.Cosmos.Container container)
        {
            database = dataBaseQuestions;
            _container = database.CreateContainerIfNotExistsAsync(ProgramApplicationConstant.containerID, ProgramApplicationConstant.partitionKey, 400).Result;
            _container = container;
        }
        public async Task<Programs> AddQuestionAsync(Programs programs)
        {
            //await _container.CreateItemAsync(question, new PartitionKey(question.Id));
            //return question;
            programs.Id = DateTime.Now.Ticks.ToString().Substring(9,6);
            try
            {
                //ScaleContainerAsync();
                ItemResponse<Programs> response = await _container.CreateItemAsync<Programs>(programs, new PartitionKey(programs.Title));
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                throw;
                //ScaleContainerAsync();
               // ItemResponse<Question> response = await _container.CreateItemAsync<Question>(question, new PartitionKey(question.Id));
            }
            return programs;
        }

        public async Task<IEnumerable<Programs>> GetQuestionsAsync()
        {
            var query = "SELECT * FROM c";
            var iterator = _container.GetItemQueryIterator<Programs>(new QueryDefinition(query));
            var results = new List<Programs>();
            while (iterator.HasMoreResults)
            {
                var response = await iterator.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }

        private async Task ScaleContainerAsync()
        {
            int? throughput = await _container.ReadThroughputAsync();
            if (throughput.HasValue)
            {

                int newThroughput = throughput.Value + 100;
                // Update throughput
                await _container.ReplaceThroughputAsync(newThroughput);

            }

        }
    }
}
