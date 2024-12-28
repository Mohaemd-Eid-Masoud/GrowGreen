using GrowGreen.Application.DTOs;
using GrowGreen.Application.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace GrowGreen.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductService _productService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(ProductService productService, IWebHostEnvironment webHostEnvironment)
        {
            _productService = productService;
            _webHostEnvironment = webHostEnvironment;
        }

        // List all products
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllProductsAsync();
            return View(products);
        }

        // Display product creation form
        public IActionResult Create()
        {
            return View();
        }

        // Handle product creation
        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> Create(ProductDto productDto, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                // Check if the file is present in the form data
                if (Request.Form.Files.Count > 0)
                {
                    image = Request.Form.Files[0];  // Manually get the file from the form
                    if (image != null && image.Length > 0)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(image.FileName);
                        string extension = Path.GetExtension(image.FileName);
                        string uniqueFileName = $"{fileName}_{Path.GetRandomFileName()}{extension}";

                        string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                        Directory.CreateDirectory(uploadsFolder); // Ensure the directory exists
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await image.CopyToAsync(fileStream);
                        }

                        productDto.ImagePath = $"/uploads/{uniqueFileName}";
                    }
                }

                await _productService.CreateProductAsync(productDto);
                return RedirectToAction(nameof(Index));
            }

            // Log validation errors
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine(error.ErrorMessage);
            }

            return View(productDto);
        }


        // Display product edit form
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // Handle product edit
        [HttpPost]
        public async Task<IActionResult> Edit(int id, ProductDto productDto, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                if (image != null && image.Length > 0)
                {
                    // Generate a unique filename
                    string fileName = Path.GetFileNameWithoutExtension(image.FileName);
                    string extension = Path.GetExtension(image.FileName);
                    string uniqueFileName = $"{fileName}_{Path.GetRandomFileName()}{extension}";

                    // Save the image to wwwroot/uploads
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(fileStream);
                    }

                    // Set the image path in the DTO
                    productDto.ImagePath = $"/uploads/{uniqueFileName}";
                }

                await _productService.UpdateProductAsync(id, productDto);
                return RedirectToAction(nameof(Index));
            }

            return View(productDto);
        }

        // Display product details
        public async Task<IActionResult> Details(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // Delete product
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // Handle product deletion
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productService.DeleteProductAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
