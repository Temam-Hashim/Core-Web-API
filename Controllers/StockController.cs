using System.IdentityModel.Tokens.Jwt;
using Google.Protobuf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Data;
using WebAPI.DTO;
using WebAPI.Extensions;
using WebAPI.Interface;
using WebAPI.Mapper;
using WebAPI.Models;
using WebAPI.Service;


namespace WebAPI.Controllers
{
    [Route("api/v1/stock")]
    [ApiController]
    public class StockController(ApplicationDBContext context, IStockRepository stockRepository, UserManager<User> userManager) : ControllerBase
    {
        private readonly ApplicationDBContext _context = context;
        private readonly IStockRepository _stockRepository = stockRepository;

        private readonly UserManager<User> _userManager = userManager;

        // [HttpGet("test")]
        // public string? Test(){
        //     var userId = User.GetUserId();
        //     return userId;
        // }

        // GET: api/v1/stock
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<StockDTO>>> GetStocks([FromQuery] string? search, int pageSize = 10, int pageIndex = 0)
        {
            // Extract user ID and role from claims
            var userId = User.GetUserId();
            var role = User.GetRole();

            if (string.IsNullOrEmpty(role))
                return Unauthorized("User role is missing in the token.");

            // Fetch stocks with role-based filtering
            var stocks = await _stockRepository.GetAllStocksAsync(search, pageSize, pageIndex, userId, role);

            // Map and return DTOs
            return Ok(stocks.Select(stock => stock.ToStockDTO()).ToList());
        }


        // GET: api/v1/stock/{id}
        [HttpGet("{id:Guid}")]
        [Authorize]
        public async Task<ActionResult<StockDTO>> GetStock(Guid id)
        {
            var userId = User.GetUserId();
            var role = User.GetRole();
            // Call the repository
            var stockDTO = await _stockRepository.GetStockByIdAsync(id, userId, role);

            // Return 404 if the stock is not found
            if (stockDTO == null) return NotFound();

            // Return the DTO
            return Ok(stockDTO);
        }


        // POST: api/v1/stock
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<StockDTO>> CreateStock([FromBody] CreateStockRequestDTO createRequestDTO)
        {
            // Use the service to create the stock
            var userId = User.GetUserId();
            var stockDTO = createRequestDTO.ToCreateStockDTO();
            stockDTO.UserId = userId;
            var stock = await _stockRepository.CreateStockAsync(stockDTO);

            // Return the created stock
            return CreatedAtAction(nameof(GetStock), new { id = stock.Id }, stock);
        }

        // PUT: api/v1/stock/{id}
        [HttpPut("{id:Guid}")]
        [Authorize]
        public async Task<IActionResult> UpdateStock([FromRoute] Guid id, [FromBody] CreateStockRequestDTO createRequestDTO)
        {
            // Get user information from the JWT token
            var userId = User.GetUserId();
            var role = User.GetRole();

            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(role))
                return (IActionResult)ApiResponseService.Error(403, "User information is missing or invalid.");

            // Check if the user is admin or owns the stock
            var stock = await _stockRepository.GetStockById(id);
            if (stock == null)
                return ApiResponseService.Error(401, "No stock found for the specified stock ID"); // NotFound("Stock not found for the specified ID.");

            if (role != "admin" && stock.UserId != userId)
                return ApiResponseService.Error(403, "You don't have permission to update this stock.");

            // Proceed with the update
            var stockDTO = createRequestDTO.ToCreateStockDTO();
            var updatedStock = await _stockRepository.UpdateStockAsync(id, stockDTO);

            if (updatedStock == null)
                return ApiResponseService.Error(500, "Failed to update stock, please try again!");

            return ApiResponseService.Success("Stock Updated successfully!", updatedStock);
        }


        // DELETE: api/v1/stock/{id}
        [HttpDelete("{id:Guid}")]
        [Authorize]
        public async Task<IActionResult> DeleteStock(Guid id)
        {
            // fetch existing stock
            var stock = _context.Stocks.FirstOrDefault(stock => stock.Id == id);
            if (stock == null) return NotFound();
            var userId = User.GetUserId();
            var role = User.GetRole();
            if (userId != stock.UserId || role != "admin")
            {
                return Unauthorized("You are not authorized to delete this stock.");
            }
            var deletedStock = await _stockRepository.DeleteStockAsync(id);
            if (deletedStock == null) return NotFound();

            return Ok(deletedStock);

        }

        // [HttpGet("filter")]
        // public async Task<ActionResult<StockDTO>> SearchStock([FromQuery] string? search=null)
        // {
        //     var stock = await _stockRepository.SearchStockAsync(search);
        //     return Ok(stock);
        // }

    }
}
