using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace AquaCultureMonitor.Core.Repositories
{
    public interface IRepository<T>
    {
        IEnumerable<T> Get();
        T Find(Expression<Func<T, bool>> match);
        List<T> FindAll(Expression<Func<T, bool>> match);
        int Add(T t);
        int Update(T t);
        int Remove(T t);
    }
}