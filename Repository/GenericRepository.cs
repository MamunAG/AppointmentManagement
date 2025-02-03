using AppointmentManagement.lib;
using ClickErp.Api.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ClickErp.Api.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly DatabaseContext _dbContext;
        private readonly DbSet<T> _entitiySet;

        public GenericRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
            _entitiySet = _dbContext.Set<T>();
        }


        public void Add(T entity)
            => _dbContext.Add(entity);


        public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
            => await _dbContext.AddAsync(entity, cancellationToken);


        public void AddRange(IEnumerable<T> entities)
            => _dbContext.AddRange(entities);


        public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
            => await _dbContext.AddRangeAsync(entities, cancellationToken);


        public bool Any(Expression<Func<T, bool>> expression)
            => _entitiySet.Any(expression);


        public async Task AnyAsync(Expression<Func<T, bool>> expression)
            => await _entitiySet.AnyAsync(expression);


        public T Get(Expression<Func<T, bool>> expression)
            => _entitiySet.FirstOrDefault(expression);


        public IEnumerable<T> GetAll()
            => _entitiySet.AsEnumerable();


        public IEnumerable<T> GetAllInclude(Expression<Func<T, object>> expression)
            => _entitiySet.Include(expression).AsEnumerable();


        public IEnumerable<T> GetAll(Expression<Func<T, bool>> expression)
            => _entitiySet.Where(expression).AsEnumerable();


        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
            => await _entitiySet.ToListAsync(cancellationToken);


        public async Task<IEnumerable<T>> GetAllIncludeAsync(Expression<Func<T, object>> expression)
            => await _entitiySet.Include(expression).ToListAsync();


        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
            => await _entitiySet.Where(expression).ToListAsync(cancellationToken);


        public async Task<T> GetAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
            => await _entitiySet.FirstOrDefaultAsync(expression, cancellationToken);


        public void Remove(T entity)
            => _dbContext.Remove(entity);


        public void RemoveRange(IEnumerable<T> entities)
            => _dbContext.RemoveRange(entities);


        public void Update(T entity) =>
            _dbContext.Entry(entity).State = EntityState.Modified;


        public void UpdateRange(IEnumerable<T> entities)
            => _dbContext.UpdateRange(entities);
    }
}
