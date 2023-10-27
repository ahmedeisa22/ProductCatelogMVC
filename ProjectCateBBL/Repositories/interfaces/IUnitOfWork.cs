using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCateBBL.Repositories.interfaces
{
    public interface IUnitOfWork
    {

        public IProductRepository product { get; }
        public ICategoryRepository category { get; }

        void save();
    }
}
