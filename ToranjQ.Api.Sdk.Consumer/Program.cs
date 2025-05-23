using System.Text.Json;
using Refit;
using ToranjQ.Api.Sdk;

var answersApi = RestService.For<IAnswersApi>("http://localhost:5109");

var answer = await answersApi.GetAnswersAsync();

Console.WriteLine(JsonSerializer.Serialize(answer));
