using CryptoBoard.Domain.Entities;
using CryptoBoard.Domain.Interfaces;
using CryptoBoard.Infra.Data.Context;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBoard.Infra.Data.Repositories
{
    public class UserRepository : IUserRepository, IDisposable
    {
        protected readonly ApplicationDbContext _applicationDbContext;
        public UserRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public User FindUser(int id)
        {
            return _applicationDbContext.users.Find(id);
        }

        public User GetName(string name)
        {
            return _applicationDbContext.users.FirstOrDefault(u => u.UserName == name);
        }

        public User GetUser(string email, string password)
        {
            return _applicationDbContext.users.FirstOrDefault(u => u.Email == email && u.Password == password);
        }

        public User GetUser(string email)
        {
            return _applicationDbContext.users.FirstOrDefault(u => u.Email == email);
        }

        public int? GetUserId(string email)
        {
            return _applicationDbContext.users.FirstOrDefault(u => u.Email == email)?.Id;
        }

        public async Task PostUser(User user)
        {
            _applicationDbContext.Add(user);
            await _applicationDbContext.SaveChangesAsync();
        }

        public User Validar(User user)
        {
            return _applicationDbContext.users.Where(u => u.Email == user.Email).FirstOrDefault();
        }

        #region Disposed https://docs.microsoft.com/pt-br/dotnet/standard/garbage-collection/implementing-dispose
        // Flag: Has Dispose already been called?
        bool disposed = false;
        // Instantiate a SafeHandle instance.
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);



        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();
                // Free any other managed objects here.
                //
            }

            disposed = true;
        }
        #endregion
    }
}
