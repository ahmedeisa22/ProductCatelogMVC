using ProductCatDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCateBBL.Repositories.interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Product getByName(string name);
        public Product getByIdWithInclue(int id);
        List<Product> getByCategory(int categoryId);
        public List<Product> getByCategoryFilterProduct(int categoryId);
        public List<Product> getCurrentProduct();
    }
}
