using Bullky.DataAccount.Repository.IRepository;
using BullyBook.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bullky.DataAccount.Repository
{
    public interface ICategoryRepository:IRepository<Category>
    {
        void Update(Category obj);
        
    }
}
