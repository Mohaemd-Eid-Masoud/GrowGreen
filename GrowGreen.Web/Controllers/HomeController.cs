using GrowGreen.Application.Services;
using GrowGreen.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrowGreen.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProductService _productService;

        public HomeController(ProductService productService)
        {
            _productService = productService;
        }

        // Displays the homepage with a list of products
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllProductsAsync();

            var productViewModels = new List<ProductViewModel>();
            foreach (var product in products)
            {
                productViewModels.Add(new ProductViewModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    ImagePath = product.ImagePath,
                    Category = product.Category,
                    Stock = product.Stock
                });
            }

            return View(productViewModels);
        }

        // About page
        public IActionResult About()
        {
            return View();
        }

        // Contact page
        public IActionResult Contact()
        {
            return View();
        }
    }
}
