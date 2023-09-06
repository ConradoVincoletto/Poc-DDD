namespace PocDDD.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
        Task SaveChangesAsync();
        Task RollbackAscync();
    }
}