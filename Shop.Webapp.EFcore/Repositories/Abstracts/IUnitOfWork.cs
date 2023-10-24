namespace Shop.Webapp.EFcore.Repositories.Abstracts
{
    public interface IUnitOfWork
    {
        bool InTransaction { get; }
        Task BeginTransactionAsync();
        Task RollBackTransactionAsync();
        Task<int> SaveChangesAsync();
    }
}
