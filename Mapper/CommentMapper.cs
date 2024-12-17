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
            return new CommentDTO
            {
                Id = commentModel.Id,
                Title = commentModel.Title,
                Content = commentModel.Content,
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