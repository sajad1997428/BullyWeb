using Bullky.DataAccount.Data;
using Bullky.DataAccount.Repository;
using BullkyBook.DataAccount.Repository.IRepository;
using BullyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BullkyBook.DataAccount.Repository
{
    public class UnitOfWork:IUnitOfWork
    {
        public ICategoryRepository Category {  get;private set; }
        public IProductRepository Product { get;private set; }
        private CLSDbContext _db;
        public UnitOfWork( CLSDbContext db)
        {
            _db = db;
            Category=new CategoryRepository(_db);
            Product=new ProductRepository(_db);
        }
        public void save()
        {
            _db.SaveChanges();
        }
    }
}
