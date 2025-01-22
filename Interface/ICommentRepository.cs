using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Interface
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllCommentsAsync();
        Task<Comment> GetCommentByIdAsync(Guid id);
        Task<List<Comment>> GetCommentByStockIdAsync(Guid stockId);

        Task<List<Comment>> GetCommentByUserIdAsync(string userId);

        Task<Comment> CreateCommentAsync(Guid stockId, string userId, Comment comment);
        Task<Comment> UpdateCommentAsync(Guid id, Comment comment);
        Task<Comment> DeleteCommentAsync(Guid id);


        Task<bool> CommentExists(Guid id);
        
    }
}