using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTO;
using WebAPI.Models;

namespace WebAPI.Interface
{
    public interface IStockRepository
    {
         Task<List<Stock>> GetAllStocksAsync(string search, int pageSize, int pageIndex, string userId, string role);
         Task<Stock?> GetStockByIdAsync(Guid id, string userId, string role);

         Task<Stock> GetStockBySymbolAsync(string symbol);
        Task<Stock?> GetStockById(Guid id);

        Task<Stock> CreateStockAsync(Stock stockModel);

        Task<Stock?> UpdateStockAsync(Guid id, Stock stockModel) ;

        Task<Stock?> DeleteStockAsync(Guid id);

        // Task<List<Stock>> SearchStockAsync(string search);

        Task<bool?> StockExist(Guid id);
    }
}