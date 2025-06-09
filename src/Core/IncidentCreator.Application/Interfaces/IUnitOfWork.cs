using IncidentCreator.Domain.Entities;

namespace IncidentCreator.Application.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync(object id);
        Task<T> FindAsync(System.Linq.Expressions.Expression<Func<T, bool>> predicate);
        Task AddAsync(T entity);
        void Update(T entity);
    }

    public interface IUnitOfWork : IDisposable
    {
        IRepository<Account> Accounts { get; }
        IRepository<Contact> Contacts { get; }
        IRepository<Incident> Incidents { get; }
        Task<int> CompleteAsync();
    }
}