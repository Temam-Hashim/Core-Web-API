
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Interface;
using WebAPI.Models;

namespace WebAPI.Repository
{
    public class StockRepository(ApplicationDBContext context) : IStockRepository
    {

        private readonly ApplicationDBContext _context = context;



        public async Task<List<Stock>> GetAllStocksAsync(string? search)
        {
            // return await _context.Stocks
            //     .OrderByDescending(s => s.CreatedAt) // Sort by CreatedAt in descending order
            //     .ToListAsync();

            if (string.IsNullOrWhiteSpace(search))
            {
                return await _context.Stocks.
                OrderByDescending(s => s.CreatedAt).
                ToListAsync(); // Return all stocks if no search term is provided.
            }

            var stocks = _context.Stocks.Where(s =>
                s.CompanyName.Contains(search) ||
                s.Industry.Contains(search) ||
                s.Symbol.Contains(search)
            );

            return await stocks.ToListAsync();
        }

        public async Task<Stock?> GetStockByIdAsync(Guid id)
        {
            // Fetch stock with related comments
            var stock = await _context.Stocks
                .Include(s => s.Comments)
                .FirstOrDefaultAsync(s => s.Id == id);

            // Return null if not found
            if (stock == null) return null;

            // Map to StockDTO
            return stock;
        }

        public async Task<Stock> CreateStockAsync(Stock stockModel)
        {
            await _context.Stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        
        }

     

        public async Task<Stock?> UpdateStockAsync(Guid id, Stock stockModel)
        {
            var existingStock = await _context.Stocks.FindAsync(id);
            if (existingStock == null) return null;

            // Update fields conditionally
            if (!string.IsNullOrEmpty(stockModel.Symbol)) existingStock.Symbol = stockModel.Symbol;
            if (!string.IsNullOrEmpty(stockModel.CompanyName)) existingStock.CompanyName = stockModel.CompanyName;
            if (stockModel.Purchase != 0) existingStock.Purchase = stockModel.Purchase;
            if (stockModel.LastDiv != 0) existingStock.LastDiv = stockModel.LastDiv;
            if (!string.IsNullOrEmpty(stockModel.Industry)) existingStock.Industry = stockModel.Industry;
            if (stockModel.MarketCap != 0) existingStock.MarketCap = stockModel.MarketCap;

            await _context.SaveChangesAsync();

            return existingStock;
        }

        public async Task<Stock?> DeleteStockAsync(Guid id)
        {
            // Find the stock entity
            var stock = await _context.Stocks.FindAsync(id);
            if (stock == null)
            {
                return null; // Return null if the stock is not found
            }

            // Remove the stock
            _context.Stocks.Remove(stock);
            await _context.SaveChangesAsync();

            // Return the deleted stock as DTO
            return stock;
        }

        // public async Task<List<Stock>> SearchStockAsync(string search)
        // {
        //     if (string.IsNullOrWhiteSpace(search))
        //     {
        //         return await _context.Stocks.ToListAsync(); // Return all stocks if no search term is provided.
        //     }

        //     var stocks = _context.Stocks.Where(s =>
        //         s.CompanyName.Contains(search) ||
        //         s.Industry.Contains(search) ||
        //         s.Symbol.Contains(search)
        //     );

        //     return await stocks.ToListAsync();
        // }

        public async Task<bool?> StockExist(Guid id)
        {
            return await  _context.Stocks.AnyAsync(x => x.Id == id);
        }

       
    }
}