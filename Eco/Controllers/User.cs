using Eco.DATA;
using Eco.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eco.Controllers
{
    public class User : Controller
    {
        private readonly AppDbContext _context;

        public User (AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var products=_context.Products.ToList();
            return View(products);
        }

        
        public IActionResult NewChart()
        {
            var products=_context.Products.ToList();

            return View(products);
        }
        public IActionResult ChartView()
        {
            var products = _context.ChartItems.Include(c => c.Product).ToList();
            return View(products);
        }

        public IActionResult Add(int id)
        {



            var product = _context.Products.Find(id);

            var existingItem = _context.ChartItems.FirstOrDefault(item => item.ProductId == id);
            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                _context.ChartItems.Add(new ChartItem { ProductId = id, Quantity = 1 });
            }
            _context.SaveChanges();
            return RedirectToAction("NewChart");
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
