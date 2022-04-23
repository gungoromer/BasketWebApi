using BasketModuleDal.Context;
using BasketModuleDal.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
using System;

namespace BasketModuleDal
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly BasketModuleContext _context;
        private IDbContextTransaction _transaction;
        public BasketModuleRepository BasketModuleRepository { get; set; }
        public UnitOfWork(BasketModuleContext context)
        {
            _context = context;
            BasketModuleRepository = new BasketModuleRepository(_context);
        }

        public void BeginTransaction()
        {
            _transaction = _context.Database.BeginTransaction();
        }

        public bool EndTransaction()
        {
            try
            {
                _transaction.Commit();
                return true;
            }
            catch
            {
                _transaction.Rollback();
                return false;
            }
        }

        public void ManuelRollback()
        {
            _transaction.Rollback();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
                _context.Dispose();
                if (_transaction != null)
                {
                    _transaction.Dispose();
                }

            }
            // free native resources if there are any.
        }
    }
}
