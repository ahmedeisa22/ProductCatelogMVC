using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProductCatDAL.Models;
using ProductCateBBL.Repositories.interfaces;
using ProductCatelogPL.ViewModel;
using System.Drawing.Printing;
using System.Security.Claims;

namespace ProductCatelogPL.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<IdentityUser> userManager;
        public readonly IMapper Imapper;
        public ProductController(IUnitOfWork _unitOfWork, UserManager<IdentityUser> _userManager,  IMapper _Imapper)
        {
            this.unitOfWork = _unitOfWork;
            this.userManager = _userManager;
            this.Imapper = _Imapper;
        }

        public IActionResult Details(int id)
        {
            var product = unitOfWork.product.getByIdWithInclue(id);

            var prdVM = Imapper.Map<ProductVM>(product);
        
            return View(prdVM);
        }
        public IActionResult Index()
        {
            List<ProductVM> productsVM = new List<ProductVM>();
            List<Category> categories = unitOfWork.category.getAll();

            var products = unitOfWork.product.getAll("Category");
            ViewData["Categories"] = categories;

            foreach (var prd in products)
            {
                var prdVM = Imapper.Map<ProductVM>(prd);
                productsVM.Add(prdVM);
            }

            return View(productsVM);
        }

        public IActionResult Create()
        {
            List<Category> categories = unitOfWork.category.getAll();
            ViewData["Categories"] = categories;
            return View();
        }
        [HttpPost]
        public IActionResult Create(ProductVM productVm)
        {
      

            if (ModelState.IsValid && productVm.CategoryId != 0)
            {

                var product = Imapper.Map<Product>(productVm);
                
                unitOfWork.product.add(product);
                unitOfWork.save();
                return RedirectToAction("Index");
            }
            else
            {
                List<Category> categories = unitOfWork.category.getAll();
                ViewData["Categories"] = categories;
                return View(productVm);
            }
        }

        public IActionResult Edit(int? Id)
        {
            if (Id == null || Id == 0)
                return NotFound();
            else
            {
                var product = unitOfWork.product.getById(Id);
                if (product == null)
                    return NotFound();
                
                var productVm = Imapper.Map<ProductVM>(product);
                List<Category> categories = unitOfWork.category.getAll();
                ViewData["Categories"] = categories;
                return View( productVm);
            }

        }
        [HttpPost]
        public IActionResult Edit(ProductVM productVm, [FromRoute] int id)
        {
            if (ModelState.IsValid && productVm.CategoryId != 0)
            {
                var productMapping = Imapper.Map<Product>(productVm);

               
                unitOfWork.product.update(productMapping);
                unitOfWork.save();
                return RedirectToAction("index");
            }
            else
            {
                List<Category> categories = unitOfWork.category.getAll();
                ViewData["Categories"] = categories;
                return View( productVm);
            }
        }


        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0)
                return NotFound();
            var product = unitOfWork.product.getById(Id);
            if (product != null)
            {
                unitOfWork.product.delete(Id);
                unitOfWork.save();
                return RedirectToAction("index");
            }
               

            return NotFound();
        }

        public IActionResult GetFilteredProductsAdmin(int categoryId)
        {
            List<Product> filteredProducts;
            List<ProductVM> productsVM = new List<ProductVM>();

            if (categoryId > 0)
            {
                filteredProducts = unitOfWork.product.getByCategory(categoryId);
            }
            else
            {
                filteredProducts = unitOfWork.product.getAll();
            }

            foreach (var prd in filteredProducts)
            {
                var prdVM = Imapper.Map<ProductVM>(prd);
                productsVM.Add(prdVM);
            }
            return PartialView("_ShowAdminProductPartial", productsVM);
        }

    }
}
