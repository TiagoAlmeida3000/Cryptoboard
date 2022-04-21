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
        Task<UserDTO> FindUserById(int id);

        Task<UserDTO> FindUserByEmail(string email);

        Task<bool> ValidateEmail(UserDTO user);

        Task<bool> ValidateUserName(UserDTO userDTO);

        Task<string> PostUser(UserDTO userDTO);

        bool ValidateHash(UserDTO userDTO, UserDTO user);

        Task<object> LoginUser(UserDTO userDTO);

        Task<string> RegisterUser(UserDTO userDTO);
    }
}
