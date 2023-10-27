using ProductCatDAL.Context;
using ProductCatDAL.Models;
using ProductCateBBL.Repositories.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCateBBL.Repositories.RepoClasses
{
    public class UnitOfWork : IUnitOfWork
    {
        private ProductContext db;
        public IProductRepository product { get; set; }
        public ICategoryRepository category { get; set; }

        public UnitOfWork(ProductContext _db)
        {
            this.db = _db;
            product = new ProductRepository(db);
            category = new CategoryRepository(db);
        }

        public void save()
        {
            this.db.SaveChanges();
        }
    }
}
