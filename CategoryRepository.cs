using Bullky.DataAccount.Data;
using BullyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bullky.DataAccount.Repository
{
    public class CategoryRepository :Repository<Category>, ICategoryRepository
    {
        private CLSDbContext _db;

       public CategoryRepository( CLSDbContext db) : base(db)
        {
            _db = db;
        }
       

        public void Update(Category obj)
        {
            _db.Categories.Update(obj);
        }
    }
}
