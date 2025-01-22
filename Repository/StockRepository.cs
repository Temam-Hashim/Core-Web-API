
using System.Security.Claims;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Interface;
using WebAPI.Models;
using WebAPI.Service;

namespace WebAPI.Repository
{
    public class StockRepository(ApplicationDBContext context) : IStockRepository
    {

        private readonly ApplicationDBContext _context = context;


        public async Task<List<Stock>> GetAllStocksAsync(string search, int pageSize, int pageIndex, string userId, string role)
        {
            var query = _context.Stocks.AsQueryable();
            // Include the User navigation property
            query = query.Include(s => s.User);

            // Role-based filtering
            if (!role.Equals("admin", StringComparison.CurrentCultureIgnoreCase) && !string.IsNullOrEmpty(userId))
            {
                query = query.Where(s => s.UserId == userId);
            }

            // Apply search filter if provided
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(s => s.CompanyName.Contains(search) || s.Symbol.Contains(search));
            }

            // Sort by creation date and paginate
            return await query
                .OrderByDescending(s => s.CreatedAt)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();
        }


        public async Task<Stock?> GetStockByIdAsync(Guid id, string userId, string role)
        {
            // Validate parameters
            if (string.IsNullOrEmpty(role)) throw new ArgumentNullException(nameof(role));
            if (string.IsNullOrEmpty(userId)) throw new ArgumentNullException(nameof(userId));
            // Fetch stock with related comments
            var stock = (Stock?)null;

            if (role == "admin")
            {
                stock = await _context.Stocks
                .Include(u => u.User)
                .Include(s => s.Comments)
                .FirstOrDefaultAsync(s => s.Id == id);
            }
            else if (role == "user")
            {
                stock = await _context.Stocks
                   .Include(u => u.User)
                   .Include(s => s.Comments)
                   .FirstOrDefaultAsync(s => s.Id == id && s.UserId == userId);
            }
            else
            {
                ApiResponseService.Error(403, "Unauthorized to access");
            }


            return stock;

            // Stock? stock = role switch
            // {
            //     "admin" => await _context.Stocks
            //                  .Include(s => s.Comments)
            //                  .FirstOrDefaultAsync(s => s.Id == id),

            //     "user" => await _context.Stocks
            //                 .Include(s => s.Comments)
            //                 .FirstOrDefaultAsync(s => s.Id == id && s.UserId == userId),

            //     _ => null // Handle invalid roles
            // };

            // Return stock or null

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
            return await _context.Stocks.AnyAsync(x => x.Id == id);
        }

        public async Task<Stock?> GetStockById(Guid id)
        {
            var stock = await _context.Stocks.FindAsync(id);
            return stock ?? null;
        }

        public async Task<Stock> GetStockBySymbolAsync(string symbol)
        {
            var result = await _context.Stocks.FirstOrDefaultAsync(x => x.Symbol == symbol);
            if (result == null)
            {
                ApiResponseService.Error(404, "Stock not found");
            }

            return result;
        }
    }
}