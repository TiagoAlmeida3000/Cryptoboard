﻿using CryptoBoard.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBoard.Domain.Entities
{
    public class User
    {
        public int Id { get;  set; }
        public string UserName { get;  set; }
        public string Email { get;  set; }
        public string Password { get;  set; }
        public DateTime CreationDate { get; set; }
        public virtual ICollection<WatchList> WatchLists { get; set; }

    }



    


}
