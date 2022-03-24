using CryptoBoard.Domain.Entities;
using CryptoBoard.Domain.Interfaces;
using CryptoBoard.Infra.Data.Context;
using Flurl.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBoard.Infra.Data.Repositories
{
    public class BinanceRepository : IBinanceRepository
    {
        ApplicationDbContext _applicationDbContext;
        public BinanceRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<Symbols>> GetListSymbols()
        {
            return await _applicationDbContext.symbols.ToListAsync();
        }

        public async Task<Symbols> GetSymbolId(int? id)
        {
            return await _applicationDbContext.symbols.FindAsync();
        }

        public async Task LoadCoinList()
        {
            var url = "https://api.binance.com/api/v3/exchangeInfo";

            var result = await url.GetJsonAsync<CoinList>();


            foreach (Symbols coin in result.symbols)
            {
                var getcoin = _applicationDbContext.symbols.FirstOrDefault(u => u.symbol == coin.symbol);
                if (getcoin == null)
                {
                    _applicationDbContext.Add(coin);
                    await _applicationDbContext.SaveChangesAsync();
                }

            }

        }
    }
}
