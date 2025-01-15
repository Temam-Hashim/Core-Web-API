using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Interface;
using WebAPI.Models;

namespace WebAPI.Repository
{
    public class UserStockRepository : IUserStockRepository
    {

        private readonly ApplicationDBContext _context;
        public UserStockRepository(ApplicationDBContext context)
        {

            _context = context;
        }
        public async Task<List<Stock>> GetUserStock(User user)
        {
            return await _context.UserStocks.Where(us => us.UserId == user.Id)
            .Select(stock => new Stock
            {
                Id = stock.StockId,
                Symbol = stock.Stock.Symbol,
                CompanyName = stock.Stock.CompanyName,
                Purchase = stock.Stock.Purchase,
                LastDiv = stock.Stock.LastDiv,
                Industry = stock.Stock.Industry,
                MarketCap = stock.Stock.MarketCap,

            }).ToListAsync();

        }
    }
}