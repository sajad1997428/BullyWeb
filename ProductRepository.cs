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
    public class ProductRepository:Repository<Product>,IProductRepository
    {
        private  CLSDbContext _db;
        public ProductRepository(CLSDbContext db):base(db) 
        {
            _db = db;
        }
        public void Update(Product obj)
        {
            
            var objFromDb=_db.Products.FirstOrDefault(u=>u.Id == obj.Id);
            if(objFromDb != null)
            {
                objFromDb.Title = obj.Title;
                objFromDb.Description = obj.Description;
                objFromDb.ISBN = obj.ISBN;
                objFromDb.price=obj.price;
                objFromDb.price50=obj.price50;
                objFromDb.price100=obj.price100;
                objFromDb.Listprice = obj.Listprice;
                objFromDb.Author=obj.Author;
                objFromDb.CategoryId=obj.CategoryId;
                if(obj.ImageUrl != null)
                {
                    objFromDb.ImageUrl = obj.ImageUrl;
                }
            }
        }
    }
}

