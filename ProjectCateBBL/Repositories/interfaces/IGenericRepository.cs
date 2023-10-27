using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProductCateBBL.Repositories.interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        T getById(int? id);

        List<T> getAll(string include = "");

        void add(T entity);

        void update(T entity);

        void delete(int? id);

   
    }
}
