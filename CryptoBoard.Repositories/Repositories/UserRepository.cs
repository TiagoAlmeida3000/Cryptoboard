using CryptoBoard.Domain.Entities;
using CryptoBoard.Domain.Interfaces;
using CryptoBoard.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
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

        public async Task<User> FindUserById(int id)
        {
            return await _applicationDbContext.users.FindAsync(id);
        }

        public async Task<User> FindUserByEmail(string email)
        {
            return await _applicationDbContext.users.Where(u => u.Email == email).FirstOrDefaultAsync();
        }

        public async Task<int> GetUserId(string email)
        {
            return await _applicationDbContext.users.Where(u => u.Email == email).Select(u => u.Id).FirstOrDefaultAsync();
        }

        public async Task<User> PostUser(User user)
        {
            var entity = _applicationDbContext.Add(user);

            await _applicationDbContext.SaveChangesAsync();

            return entity.Entity;
        }

        public async Task<bool> ValidateEmail(string email)
        {
            return await _applicationDbContext.users.AnyAsync(u => u.Email == email);
        }

        public async Task<bool> ValidateUserName(string userName)
        {
            return await _applicationDbContext.users.AnyAsync(u => u.UserName == userName);
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
