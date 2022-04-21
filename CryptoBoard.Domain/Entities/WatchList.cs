using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBoard.Domain.Entities
{
    public class WatchList
    {
        public int Id { get; set; }

        public int SymbolsId { get; set; }

        public DateTime CreationDate { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }
    }
}
