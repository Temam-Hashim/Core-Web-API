using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Data;
using WebAPI.DTO;
using WebAPI.Interface;
using WebAPI.Mapper;


namespace WebAPI.Controllers
{
    [Route("api/v1/stock")]
    [ApiController]
    public class StockController(ApplicationDBContext context, IStockRepository stockRepository) : ControllerBase
    {
        private readonly ApplicationDBContext _context = context;
        private readonly IStockRepository _stockRepository = stockRepository;

        // GET: api/v1/stock
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<StockDTO>>> GetStocks([FromQuery] string? search, int pageSize, int pageIndex)
        {
            var stocks = await _stockRepository.GetAllStocksAsync(search, pageSize, pageIndex);  //.ConfigureAwait(false);
            var stockDTOs = stocks.Select(stock => stock.ToStockDTO()).ToList();
            return Ok(stockDTOs);
        }

        // GET: api/v1/stock/{id}
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<StockDTO>> GetStock(Guid id)
        {
            // Call the repository
            var stockDTO = await _stockRepository.GetStockByIdAsync(id);

            // Return 404 if the stock is not found
            if (stockDTO == null) return NotFound();

            // Return the DTO
            return Ok(stockDTO);
        }


        // POST: api/v1/stock
        [HttpPost]
        public async Task<ActionResult<StockDTO>> CreateStock([FromBody] CreateStockRequestDTO createRequestDTO)
        {
            // Use the service to create the stock
            var stockDTO = createRequestDTO.ToCreateStockDTO();
            var stock = await _stockRepository.CreateStockAsync(stockDTO);
           
            // Return the created stock
            return CreatedAtAction(nameof(GetStock), new { id = stock.Id }, stock);
        }

        // PUT: api/v1/stock/{id}
        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> UpdateStock([FromRoute] Guid id, [FromBody] CreateStockRequestDTO createRequestDTO)
        {
            var stockDTO = createRequestDTO.ToCreateStockDTO();
            var existingStock = await _stockRepository.UpdateStockAsync(id, stockDTO);
            if (existingStock == null) return NotFound();
            return Ok(existingStock);
        }

        // DELETE: api/v1/stock/{id}
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteStock(Guid id)
        {
        
                var stock = await _stockRepository.DeleteStockAsync(id);
                if (stock == null) return NotFound();

                return Ok(stock);
            
        
        }

        // [HttpGet("filter")]
        // public async Task<ActionResult<StockDTO>> SearchStock([FromQuery] string? search=null)
        // {
        //     var stock = await _stockRepository.SearchStockAsync(search);
        //     return Ok(stock);
        // }

    }
}
