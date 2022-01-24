using Demo.API.Models;

namespace Demo.API.Services
{
    public interface ICommentsService
    {
        Task<List<Comment>> GetAll();

        Task<List<Comment>> GetByPost(int postId);

        Task<Comment> GetById(int commentId);
    }
}
