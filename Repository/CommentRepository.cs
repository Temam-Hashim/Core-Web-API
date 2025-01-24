using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Interface;
using WebAPI.Models;
using WebAPI.Service;

namespace WebAPI.Repository
{
    public class CommentRepository(ApplicationDBContext context) : ICommentRepository
    {
        private readonly ApplicationDBContext _context = context;



        public async Task<List<Comment>> GetAllCommentsAsync()
        {
            return await _context.Comments.Include(u => u.User).ToListAsync();

        }

        public async Task<Comment> GetCommentByIdAsync(Guid id)
        {
            var comment = await _context.Comments
                                      //   .Include(c => c.Stock)
                                      .Include(c => c.User)
                                     .FirstOrDefaultAsync(c => c.Id == id);
            if (comment == null) return null;

            return comment;
        }



        public async Task<List<Comment>> GetCommentByStockIdAsync(Guid stockId)
        {
            var comments = await _context.Comments
                                            //                  .Include(c => c.Stock)
                                            .Include(c => c.User)
                                            .Where(c => c.StockId == stockId)
                                            .ToListAsync();

            return comments;
        }

        public async Task<List<Comment>> GetCommentByUserIdAsync(string userId)
        {
            var comments = await _context.Comments.Include(u => u.User).Where(c => c.UserId == userId).ToListAsync(); ;
            if (comments == null) return null;
            return comments;
        }

        public async Task<Comment> CreateCommentAsync(Guid stockId, string userId, Comment comment)
        {
            // Assign the stockId to the comment
            comment.StockId = stockId;
            comment.UserId = userId;

            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();

            // Ensure the User is loaded after the comment is saved
            var createdComment = await _context.Comments
                .Include(c => c.User)  // Ensure User is included
                .FirstOrDefaultAsync(c => c.Id == comment.Id);

            return createdComment;  // Return the created comment with its associated User
        }

        public async Task<Comment> UpdateCommentAsync(Guid id, Comment commentRequest)
        {
            var existingComment = await _context.Comments.FindAsync(id);
            if (existingComment == null) return null;

            // Update fields only if they are not null or default
            existingComment.Title = string.IsNullOrEmpty(commentRequest.Title) ? existingComment.Title : commentRequest.Title;
            existingComment.Content = string.IsNullOrEmpty(commentRequest.Content) ? existingComment.Content : commentRequest.Content;

            // _context.Comments.Update(existingComment);
            await _context.SaveChangesAsync();


            return existingComment;

        }

        public async Task<Comment> DeleteCommentAsync(Guid id)
        {
            var existingComment = _context.Comments.FirstOrDefault(c => c.Id == id);
            if (existingComment == null) return null;

            _context.Comments.Remove(existingComment);
            await _context.SaveChangesAsync();
            return existingComment;
        }
      

        public async Task<bool> CommentExists(Guid id)
        {
            return await _context.Comments.AnyAsync(c => c.Id == id);
        }


    }
}