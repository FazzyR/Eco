using Eco.DATA;
using Eco.Models;
using Microsoft.AspNetCore.Mvc;

namespace Eco.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var Products=_context.Products.ToList();

            return View(Products);
        }

        public IActionResult AddForm () { return View(); }

        [HttpPost]
        public IActionResult Add(Product product)
        {
            

            _context.Products.Add(product);

            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        
        public IActionResult Delete(int id) {


            var productToDelete = _context.Products.Find(id);

            if (productToDelete == null)
            {
                // Handle the case where the product with the given ID is not found
                return NotFound();
            }

            // Remove the product from the context
            _context.Products.Remove(productToDelete);

            // Save changes to the database
            _context.SaveChanges();

            // Redirect to the product listing page
            return RedirectToAction("Index");
        }

        public IActionResult EditForm(Product product)
        {
            return View();
        }

        public IActionResult Edit(int id, Product product)
        {
            _context.Products.Find(id).Name = product.Name;
            _context.Products.Find(id).Description = product.Description;

            _context.Products.Find(id).Price = product.Price;
            _context.Products.Find(id).ImageURL = product.ImageURL;



            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

    }
}
