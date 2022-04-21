using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBoard.Application.DTOs
{
    public class SymbolsDTO
    {
        public int id { get; set; }

        public string symbol { get; set; }

        public string status { get; set; }

        public string baseAsset { get; set; }

        public int baseAssetPrecision { get; set; }

        public string quoteAsset { get; set; }

        public int quotePrecision { get; set; }

        public int quoteAssetPrecision { get; set; }

        public int baseCommissionPrecision { get; set; }

        public int quoteCommissionPrecision { get; set; }
    }
}
