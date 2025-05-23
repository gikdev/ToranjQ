using Refit;
using ToranjQ.Contracts.Requests;
using ToranjQ.Contracts.Responses;

namespace ToranjQ.Api.Sdk;

public interface IAnswersApi
{
   [Get(ApiEndpoints.Answers.Get)]
   Task<AnswerResponse> GetAnswerAsync(Guid id);
   
   [Get(ApiEndpoints.Answers.GetAll)]
   Task<AnswersResponse> GetAnswersAsync();
   
   [Post(ApiEndpoints.Answers.Create)]
   Task<AnswersResponse> CreateAnswerAsync(CreateAnswerRequest request);
   
   [Put(ApiEndpoints.Answers.Update)]
   Task<AnswersResponse> UpdateAnswerAsync(Guid id, UpdateAnswerRequest request);
   
   [Delete(ApiEndpoints.Answers.Delete)]
   Task DeleteAnswerAsync(Guid id);
}
