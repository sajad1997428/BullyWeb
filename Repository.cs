using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Bullky.DataAccount.Data;
using Bullky.DataAccount.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Bullky.DataAccount.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly CLSDbContext _db;
        internal DbSet<T> dbset;
        public Repository(CLSDbContext db) 
        { 
        _db = db;
            this.dbset=_db.Set<T>();
            // _db.Categories == dbset;
            _db.Products.Include(u => u.Category).Include(u => u.CategoryId);
        }
        public void Add(T entity)
        {
           dbset.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> filter, string? includeProperies = null)
        {
            IQueryable<T> query = dbset;

            query=query.Where(filter);
            if (!string.IsNullOrEmpty(includeProperies))
            {
             foreach(var includeprop in includeProperies.Split(new char[] {','},StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeprop);
                }
           
            }
            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll(string? includeProperies=null)
        {
            IQueryable<T> query=dbset;
            if(!string.IsNullOrEmpty(includeProperies))
            {
                foreach(var includeprop in includeProperies.Split(new char[] {','} ,StringSplitOptions.RemoveEmptyEntries))
                {
                    query=query.Include(includeprop);
                }
            }
            return query.ToList();
        }

        public void Remove(T entity)
        {
            dbset.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
           dbset.RemoveRange(entity);
        }

       
    }
}
