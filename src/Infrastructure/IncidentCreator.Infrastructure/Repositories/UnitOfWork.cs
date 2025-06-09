using IncidentCreator.Application.Interfaces;
using IncidentCreator.Domain.Entities;
using IncidentCreator.Infrastructure.Data;
using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace IncidentCreator.Infrastructure.Repositories
{
    public class Repository<T>(ApplicationDbContext context) : IRepository<T>
        where T : class
    {
        protected readonly ApplicationDbContext _context = context;
        public async Task AddAsync(T entity) => await _context.Set<T>().AddAsync(entity);
        public async Task<T> GetByIdAsync(object id) => await _context.Set<T>().FindAsync(id);
        public void Update(T entity) => _context.Set<T>().Update(entity);
        public async Task<T> FindAsync(Expression<Func<T, bool>> predicate) => await _context.Set<T>().FirstOrDefaultAsync(predicate);
    }
    
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IRepository<Account> Accounts { get; private set; }
        public IRepository<Contact> Contacts { get; private set; }
        public IRepository<Incident> Incidents { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Accounts = new Repository<Account>(_context);
            Contacts = new Repository<Contact>(_context);
            Incidents = new Repository<Incident>(_context);
        }

        public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();
        public void Dispose() => _context.Dispose();
    }
}