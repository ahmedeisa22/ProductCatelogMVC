using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProductCatDAL.Models;
using ProductCateBBL.Repositories.interfaces;
using ProductCatelogPL.ViewModel;
using ProjectCatelogMVC.Models;
using System.Diagnostics;

namespace ProjectCatelogMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<IdentityUser> userManager;
        public readonly IMapper Imapper;


        public HomeController(IUnitOfWork _unitOfWork, UserManager<IdentityUser> _userManager, IMapper _Imapper)
        {
            this.unitOfWork = _unitOfWork;
            userManager = _userManager;
            this.Imapper = _Imapper;
        }

        public IActionResult Index()
        {
           
            var products = unitOfWork.product.getCurrentProduct();
            List<ProductVM> productsVM = new List<ProductVM>();
            List<Category> categories = unitOfWork.category.getAll();
            ViewData["Categories"] = categories;

            foreach (var prd in products)
            {
                var prdVM = Imapper.Map<ProductVM>(prd);
                productsVM.Add(prdVM);
            }

            return View(productsVM);
        }

        public IActionResult Details(int id)
        {
            var product = unitOfWork.product.getByIdWithInclue(id);
            var prdVM = Imapper.Map<ProductVM>(product);
            return View(prdVM);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult GetFilteredProductsUser(int categoryId)
        {
            List<Product> filteredProducts;
            List<ProductVM> productsVM = new List<ProductVM>();

            if (categoryId > 0)
            {
                filteredProducts = unitOfWork.product.getByCategoryFilterProduct(categoryId);
            }
            else
            {
                filteredProducts = unitOfWork.product.getCurrentProduct();
            }

            foreach (var prd in filteredProducts)
            {
                var prdVM = Imapper.Map<ProductVM>(prd);
                productsVM.Add(prdVM);
            }
            return PartialView("_ShowUserProductPartial", productsVM);
        }

    }
}