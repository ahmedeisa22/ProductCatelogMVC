using Microsoft.EntityFrameworkCore;
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
    public class ProductRepository : IProductRepository
    {
        private readonly ProductContext context;
        public ProductRepository(ProductContext _context)
        {
            this.context = _context;
        }

        public Product getById(int? id)
        {
            var product = context.Product.FirstOrDefault(p => p.Id == id);
            if (product != null)
                return product;

            return null;
        }
        public Product getByIdWithInclue(int id)
        {
            var product = context.Product.Include(p=>p.Category).Include(p=>p.User).FirstOrDefault(p => p.Id == id);
            if (product != null)
                return product;

            return null;
        }

        public Product getByName(string name)
        {
            Product product = context.Product.FirstOrDefault(p => p.Name == name);
            if (product != null)
                return product;

            return null;
        }


        public List<Product> getAll(string include = "")
        {

            var query = context.Product.AsQueryable();
            if (!String.IsNullOrEmpty(include))
            {
                var includes = include.Split(",");
                foreach (var inc in includes)
                {
                    query = query.Include(inc.Trim());
                }
            }
            return query.ToList();

        }

        public void add(Product entity)
        {

            if (entity != null)
            {
                context.Product.Add(entity);

            }

        }

        public void delete(int? id)
        {
            var product = getById(id);
            if (product != null)
            {
                context.Product.Remove(product);
            }


        }


        public void update(Product entity)
        {

            if (entity != null)
                context.Product.Update(entity);

        }

        public Product getbyid(int id)
        {
            return context.Product.FirstOrDefault(r => r.Id == id);
        }

        public List<Product> search(string search)
        {
            return context.Product.Where(p => p.Name.Contains(search)).ToList();
        }

        public List<Product> getByCategory(int categoryId)
        {
            return context.Product.Where(p => p.CategoryId == categoryId).ToList();
        }
        public List<Product> getByCategoryFilterProduct(int categoryId)
        {
            return getCurrentProduct().Where(p => p.CategoryId == categoryId).ToList();
        }
        public List<Product> getCurrentProduct()
        {
            var currentDateTime = DateTime.Now;
            var CurrentProudcts =new List<Product>();

            foreach (var p in context.Product)
            {
                if(p.StartDate <= currentDateTime && currentDateTime <= p.StartDate.Add(p.Duration))
                    CurrentProudcts.Add(p);
                
            }

            return CurrentProudcts;
        }
    }
}
