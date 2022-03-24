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
        User FindUser(int id);
        int? GetUserId(string email);
        User GetUser(string email, string password);
        User GetUser(string email);
        User GetName(string name);
        User Validar(User user);
        Task PostUser(User user);
    }
}
