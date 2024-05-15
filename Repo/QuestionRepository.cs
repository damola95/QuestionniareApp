﻿using QuestionnaireApp.Model;
using System.Collections.Concurrent;
using Microsoft.Azure.Cosmos;
using  QuestionnaireApp.Interface;
using System.Net;
using QuestionnaireApp.Constants;

namespace QuestionnaireApp.Repo
{
    public class QuestionRepository : IQuestionRepository
    {
        private  Container _container;
        private Database database;

        public QuestionRepository(CosmosClient cosmosClient, Database dataBaseQuestions, Microsoft.Azure.Cosmos.Container container)
        {
            database = dataBaseQuestions;
            _container = database.CreateContainerIfNotExistsAsync(QuestionsConstants.containerID, QuestionsConstants.partitionKey, 400).Result;
            _container = container;   
        }

        public async Task<Question> AddQuestionAsync(Question question)
        {
            question.Id = DateTime.Now.Ticks.ToString().Substring(9,6);
            
            //await _container.CreateItemAsync(question, new PartitionKey(question.Id));
            //return question;
            try
            {
                ScaleContainerAsync();
                ItemResponse<Question> response = await _container.CreateItemAsync<Question>(question, new PartitionKey(question.Id));
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                ScaleContainerAsync();
                ItemResponse<Question> response = await _container.CreateItemAsync<Question>(question, new PartitionKey(question.Id));
            }
            return question;

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
        public async Task<Question> UpdateQuestionAsync(Question question)
        {
            await _container.UpsertItemAsync(question, new PartitionKey(question.Id));
            return question;
        }

        public async Task<IEnumerable<Question>> GetQuestionsAsync()
        {
            var query = "SELECT * FROM c";
            var iterator = _container.GetItemQueryIterator<Question>(new QueryDefinition(query));
            var results = new List<Question>();
            while (iterator.HasMoreResults)
            {
                var response = await iterator.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }

        public async Task<ApplicationData> AddApplicationDataAsync(ApplicationData applicationData)
        {
            applicationData.Id = Guid.NewGuid().ToString();
            await _container.CreateItemAsync(applicationData, new PartitionKey(applicationData.Id));
            return applicationData;
        }

     
    }
}
