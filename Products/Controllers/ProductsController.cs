using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Models;


namespace Products.Controllers
{
    public class ProductsController : Controller
    {
        // index
        
        public IActionResult Index()
        {
            List<Product> products = getProducts();
            
            return View(products);
        }

        private readonly IProductService _productService;
        
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        
        private List<Product> getProducts()
        {
            return _productService.getProducts();
        }
        
        // add product

        public IActionResult AddProduct()
        {
            return View();
        }
        
        // POST add product
        [HttpPost]
        public IActionResult AddProduct(Product newProduct)
        {
            if (ModelState.IsValid)
            {
                _productService.addProduct(newProduct);
                
                return RedirectToAction("Index");
            }
            
            return View(newProduct);
        }
        
        
        // edit
        
        public IActionResult Edit(int id)
        {
            Product productToEdit = GetProductById(id);

            if (productToEdit == null)
            {
                return NotFound();
            }
            
            return View(productToEdit);
        }
        
        private Product GetProductById(int id)
        {
            Product product = _productService.getProductById(id);

            return product;
        }
        
        [HttpPost]
        public IActionResult Edit(Product editedProduct)
        {
            if (ModelState.IsValid)
            {
                _productService.editProduct(editedProduct);
                
                return RedirectToAction("Index");
            }
            
            return View(editedProduct);
        }
        
    }
}