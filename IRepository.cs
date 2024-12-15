using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bullky.DataAccount.Repository.IRepository
{
    public interface IRepository<T>where T : class
    {
        IEnumerable<T> GetAll(string? includeProperies = null);
        T Get(Expression<Func<T, bool>> filter, string? includeProperies = null);
        void Add(T entity);
       
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);
    }
}
