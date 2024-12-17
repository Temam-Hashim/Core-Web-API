using WebAPI.Models;
using WebAPI.DTO;

namespace WebAPI.Mapper
{
    public static class StockMapper
    {
        public static StockDTO ToStockDTO(this Stock stockModel)
        {
            return new StockDTO
            {
                Id = stockModel.Id,
                Symbol = stockModel.Symbol,
                CompanyName = stockModel.CompanyName,
                Purchase = stockModel.Purchase,
                LastDiv = stockModel.LastDiv,
                Industry = stockModel.Industry,
                MarketCap = stockModel.MarketCap,
                // Comments = stockModel.Comments.Select(c => c.ToCommentDTO()).ToList()
            };
        }

        public static Stock ToCreateStockDTO(this CreateStockRequestDTO createStockRequest)
        {
            return new Stock
            {
                Symbol = createStockRequest.Symbol,
                CompanyName = createStockRequest.CompanyName,
                Purchase = createStockRequest.Purchase,
                LastDiv = createStockRequest.LastDiv,
                Industry = createStockRequest.Industry,
                MarketCap = createStockRequest.MarketCap
            };
        }
    }
}
