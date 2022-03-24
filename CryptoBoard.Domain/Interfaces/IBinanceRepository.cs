using CryptoBoard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBoard.Domain.Interfaces
{
    public interface IBinanceRepository
    {
        Task LoadCoinList();
        Task<Symbols> GetSymbolId(int? id);
        Task<IEnumerable<Symbols>> GetListSymbols();
    }
}
