using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ProductCatDAL.Models;
using ProductCatelogPL.Models;

namespace ProductCatelogPL.ViewModel
{
    public class AutoMapperProfiler : Profile
    {
        public AutoMapperProfiler()
        {
           
            CreateMap<UserVM, IdentityUser>();
            CreateMap<Product, ProductVM>();
            CreateMap<ProductVM, Product > ();

        }

    }
}
