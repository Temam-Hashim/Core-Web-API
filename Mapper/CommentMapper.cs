using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.DTO;
using WebAPI.DTO.Comment;

namespace WebAPI.Mapper
{
    public static class CommentMapper
    {

        
        public static CommentDTO ToCommentDTO(this Comment commentModel){
            // Check if the User is null to avoid the NullReferenceException
            return new CommentDTO
            {
                Id = commentModel.Id,
                Title = commentModel.Title,
                Content = commentModel.Content,
                CreatedBy = commentModel.User != null ? commentModel.User.FirstName + " " + commentModel.User.LastName : "Anonymous",
                UserId = commentModel.User?.Id,
                CreatedAt = commentModel.CreatedAt,
                StockId = commentModel.StockId
            };
        }


        public static Comment ToComment(this CommentDTO commentDTO)
        {
            return new Comment
            {
                Title = commentDTO.Title,
                Content = commentDTO.Content,
                StockId = commentDTO.StockId,
                CreatedAt = commentDTO.CreatedAt
            };
        }

        public static Comment ToCreateComment(this CreateCommentRequestDTO commentDTO)
        {
            return new Comment
            {
                Title = commentDTO.Title,
                Content = commentDTO.Content
            };
        }
    }
}