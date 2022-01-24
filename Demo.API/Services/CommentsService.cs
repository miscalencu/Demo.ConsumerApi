using Demo.API.Models;
using Demo.API.Extensions;

namespace Demo.API.Services
{
    public class CommentsService : ICommentsService
    {
        private readonly HttpClient _commentsHttpClient;

        public CommentsService(IHttpClientFactory httpClientFactory)
        {
            _commentsHttpClient = httpClientFactory.CreateClient("commentsApi");
        }

        public async Task<List<Comment>> GetAll()
        {
            var response = await _commentsHttpClient.GetAsync("");
            response.EnsureSuccessStatusCode();
            return await response.ConvertResponse<List<Comment>>();
        }
        public Task<List<Comment>> GetByPost(int postId)
        {
            throw new NotImplementedException();
        }

        public async Task<Comment> GetById(int commentId)
        {
            var response = await _commentsHttpClient.GetAsync($"{commentId}");
            response.EnsureSuccessStatusCode();
            return await response.ConvertResponse<Comment>();
        }

    }
}
