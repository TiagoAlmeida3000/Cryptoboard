using CryptoBoard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBoard.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User> FindUserById(int id);

        Task<User> FindUserByEmail(string email);

        Task<int> GetUserId(string email);

        Task<bool> ValidateEmail(string email);

        Task<bool> ValidateUserName(string userName);

        Task<User> PostUser(User user);
    }
}
