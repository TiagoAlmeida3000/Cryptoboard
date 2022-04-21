using CryptoBoard.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBoard.Application.Interfaces
{
    public interface IBinanceService
    {
        Task LoadCoinList();

        Task<SymbolsDTO> GetSymbolId(int? id);

        Task<IEnumerable<SymbolsDTO>> GetListSymbols(int skip, int take);

        Task<int> CountSymbols();

        Task<object> CryptocurrenciesData(int skip, int take);
    }
}
