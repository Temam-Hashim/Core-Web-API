using WebAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;
using WebAPI.DTO;
using System;
using WebAPI.Mapper;
using WebAPI.Interface;
using WebAPI.DTO.Comment;

namespace WebAPI.Controllers
{
    // [Route("api/v1/[controller]")]
    [Route("api/v1/comment")]
    [ApiController]
    public class CommentController(ApplicationDBContext context, ICommentRepository commentRepository, IStockRepository stockRepository) : ControllerBase
    {
        private readonly ApplicationDBContext _context = context;
        private readonly ICommentRepository _commentRepository = commentRepository;

        private readonly IStockRepository _stockRepository = stockRepository;

        // GET: api/Comment
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommentDTO>>> GetComments()
        {
            var comments = await _commentRepository.GetAllCommentsAsync();
            var commentDTOs = comments.Select(comment => comment.ToCommentDTO()).ToList();
            return Ok(commentDTOs);
        }

        // GET: api/Comment/{id}
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<Comment>> GetComment(Guid id)
        {
            var comment = await _commentRepository.GetCommentByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            return comment;
        }

        // PUT: api/Comment/{id}
        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> PutComment(Guid id, [FromBody] CreateCommentRequestDTO commentRequest)
        {
            // Check if the stock exists using _stockRepository
            var stockExists = await _stockRepository.StockExist(id);
            if (stockExists != true) // Checks for null or false
            {
                return NotFound($"Stock with ID {id} not found.");
            }
            var comment = commentRequest.ToCreateComment();
            var existingComment = await _commentRepository.UpdateCommentAsync(id, comment);
            if (existingComment == null) return NotFound();

            return Ok(existingComment.ToCommentDTO());
        }

        // POST: api/Comment
        [HttpPost("{stockId:Guid}")]
        public async Task<ActionResult<CommentDTO>> PostComment([FromRoute] Guid stockId, [FromBody] CreateCommentRequestDTO commentRequest)
        {
            // Check if the stock exists using _stockRepository
            var stockExists = await _stockRepository.StockExist(stockId);
            if (stockExists != true) // Checks for null or false
            {
                return NotFound($"Stock with ID {stockId} not found.");
            }

            // Map CreateCommentRequestDTO to Comment entity
            var comment = commentRequest.ToCreateComment();

            // Save the comment using the repository
            var newComment = await _commentRepository.CreateCommentAsync(stockId, comment);

            // Map the newly created Comment to CommentDTO for the response
            return CreatedAtAction(nameof(GetComment), new { id = newComment.Id }, newComment.ToCommentDTO());
        }




        // DELETE: api/Comment/{id}
        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult<Comment>> DeleteComment(Guid id)
        {
            var comment = await _commentRepository.DeleteCommentAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            return comment;
        }



        // GET: api/comment/stock/{stockId}
        [HttpGet("stock/{stockId:Guid}")]
        public async Task<ActionResult<IEnumerable<Comment>>> GetCommentsByStockId(Guid stockId)
        {
            // Fetch all comments associated with the given stock ID
            var comments = await _commentRepository.GetCommentByStockIdAsync(stockId);

            if (comments == null || comments.Count == 0)
            {
                return NotFound();
            }

            return Ok(comments);
        }



    }
}
