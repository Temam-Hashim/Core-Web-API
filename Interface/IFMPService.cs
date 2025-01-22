using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Interface
{
    public interface IFMPService
    {
         Task<Stock> GetStockBySymbolAsync(string symbol);
    }
}