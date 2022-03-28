using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBoard.Application.DTOs
{
    public class WatchListDTO
    {
        public int Id { get; set; }
        public int SymbolsId { get; set; }
        public DateTime CreationDate { get; set; }
        public int UserId { get; set; }
    }
}
