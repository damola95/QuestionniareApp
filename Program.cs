using Microsoft.Azure.Cosmos;
using QuestionnaireApp.Interface;
using QuestionnaireApp.Repo;
using QuestionnaireApp.Service;
using FluentValidation;
using FluentValidation.AspNetCore;
using QuestionnaireApp.Validator;
using QuestionnaireApp.MiddleWare;
using static Azure.Core.HttpHeader;
using QuestionnaireApp.Model;
using System.Reflection.Metadata;
using QuestionnaireApp.Constants;
using Microsoft.Azure.Cosmos.Serialization.HybridRow.Schemas;
using System.ComponentModel;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.DependencyInjection;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var cosmosClientOptions = new CosmosClientOptions
{
    ConnectionMode = ConnectionMode.Direct,
    MaxRetryAttemptsOnRateLimitedRequests = 9,
    MaxRetryWaitTimeOnRateLimitedRequests = TimeSpan.FromSeconds(30)
};

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<QuestionDtoValidator>());
builder.Services.AddSingleton(s => new CosmosClient(builder.Configuration["CosmosDb:ConnectionString"]));
builder.Services.AddSingleton(s => new CosmosClient(CosmosClientConstant.endpointUri, cosmosClientOptions));
builder.Services.AddScoped<IQuestionService, QuestionService>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<IProgramApplicationService, ProgramsApplicationService>();
builder.Services.AddScoped<IProgramApplicationRepo, ApplicationRepo>();
//builder.Services.AddScoped<Database>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var cosmosClient = new CosmosClient(CosmosClientConstant.endpointUri, CosmosClientConstant.primaryKey, new CosmosClientOptions()
{
    ApplicationName = "QuestionnaireApp"
});
Database DataBase = cosmosClient.CreateDatabaseIfNotExistsAsync(QuestionsConstants.DB).Result;
//Microsoft.Azure.Cosmos.Container QuestionsContainer = DataBase.CreateContainerIfNotExistsAsync(QuestionsConstants.containerID, QuestionsConstants.partitionKey, 400).Result;
builder.Services.AddSingleton<CosmosClient>(cosmosClient);
builder.Services.AddSingleton<Database>(DataBase);
//builder.Services.AddSingleton<Microsoft.Azure.Cosmos.Container>(QuestionsContainer);


Database ProgramsDataBase = cosmosClient.CreateDatabaseIfNotExistsAsync(ProgramApplicationConstant.DB).Result;
Microsoft.Azure.Cosmos.Container ProgramsContainer = DataBase.CreateContainerIfNotExistsAsync(ProgramApplicationConstant.containerID, ProgramApplicationConstant.partitionKey, 400).Result;
builder.Services.AddSingleton<CosmosClient>(cosmosClient);
builder.Services.AddSingleton<Database>(ProgramsDataBase);
builder.Services.AddSingleton<Microsoft.Azure.Cosmos.Container>(ProgramsContainer);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}
else
{
    app.UseMiddleware<ErrorHandlingMiddleware>();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();