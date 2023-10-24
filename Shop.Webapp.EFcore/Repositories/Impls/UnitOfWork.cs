using Microsoft.EntityFrameworkCore.Storage;
using Shop.Webapp.EFcore.Repositories.Abstracts;
using Shop.Webapp.Shared.Exceptions;

namespace Shop.Webapp.EFcore.Repositories.Impls
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IDbContextTransaction _transaction;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public bool InTransaction => _transaction != null;

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task RollBackTransactionAsync()
        {
            await _transaction.RollbackAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            if (_transaction == null)
                throw new CustomerException("Transaction is not open");

            try
            {
                await _transaction.CommitAsync();
                var result = await _context.SaveChangesAsync();

                _transaction = null;
                return result;
            }
            catch
            {
                await RollBackTransactionAsync();
                throw;
            }
        }
    }
}
