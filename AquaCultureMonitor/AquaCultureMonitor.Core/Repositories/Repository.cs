using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace AquaCultureMonitor.Core.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DatabaseContext _dbContext;

        public Repository(DatabaseContext context)
        {
            _dbContext = context;
        }

        public IEnumerable<T> Get()
        {
            return _dbContext.Set<T>().ToList();
        }

        public T Find(Expression<Func<T, bool>> match)
        {
            return _dbContext.Set<T>().SingleOrDefault(match);
        }

        public List<T> FindAll(Expression<Func<T, bool>> match)
        {
            return _dbContext.Set<T>().Where(match).ToList();
        }

        public int Add(T t)
        {
            _dbContext.Set<T>().Add(t);
            return _dbContext.SaveChanges();
        }

        public int Update(T t)
        {
            _dbContext.Entry(t).State = EntityState.Modified;
            return _dbContext.SaveChanges();
        }

        public int Remove(T t)
        {
            _dbContext.Entry(t).State = EntityState.Deleted;
            return _dbContext.SaveChanges();
        }
    }
}
