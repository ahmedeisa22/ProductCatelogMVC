using Microsoft.EntityFrameworkCore;
using ProductCatDAL;
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
    public class CategoryRepository:ICategoryRepository
    {
        private readonly ProductContext context;
        public CategoryRepository(ProductContext _context)
        {
            this.context = _context;
            
        }

        public void add(Category entity)
        {
            throw new NotImplementedException();
        }

        public void delete(int? id)
        {
            throw new NotImplementedException();
        }

        public List<Category> getAll(string include = "")
        {
            var query = context.Category.AsQueryable();
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

        public Category getById(int? id)
        {
            var category = context.Category.FirstOrDefault(p => p.CategoryId == id);
            if (category != null)
                return category;

            return null;
        }

    

        public void update(Category entity)
        {
            throw new NotImplementedException();
        }
        

    }
}
