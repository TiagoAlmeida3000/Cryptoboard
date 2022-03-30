using CryptoBoard.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBoard.Application.Interfaces
{
    public interface IUserService
    {
        UserDTO FindUser(int id);
        int? GetUserId(string email);
        UserDTO GetUser(string email, string password);
        UserDTO GetUser(string email);
        UserDTO GetName(string name);
        UserDTO Validar(UserDTO user);
        Task PostUser(UserDTO user);
        string CreateToken(UserDTO userDTO);
        bool ValidateHash(UserDTO userDTO, UserDTO user);
        UserDTO NewUser(UserDTO userDTO);
    }
}
