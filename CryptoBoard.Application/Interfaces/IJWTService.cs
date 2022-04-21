using CryptoBoard.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBoard.Application.Interfaces
{ 
    public interface IJWTService 
    {
        string CreateToken(UserDTO userDTO);
    }
}
