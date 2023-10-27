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
        public IProductRepository product { get; }
        public ICategoryRepository category { get; }

        public UnitOfWork(ProductContext _db, IProductRepository _proudct, ICategoryRepository _category)
        {
            this.db = _db;
            this.product = _proudct;
            this.category = _category;
        }

        public void save()
        {
            this.db.SaveChanges();
        }
    }
}
