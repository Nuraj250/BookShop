using AdminPanelApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanelApp.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            var product = new Product
            {
                Id = id,
                Title = "Dark in Death: An Eve Dallas Novel (In Death, Book 46)",
                Author = "J. D. Robb",
                Price = 1400.20M,
                Description = "Dark in Death: An Eve Dallas Novel (In Death, Book 46)",
                ImageUrl = "/images/book1.jpg",
                Features = new List<string>
            {
                "Free Delivery - Orders over $100",
                "Secure Payment - 100% Secure Payment",
                "Money Back Guarantee - Within 30 Days",
                "24/7 Support - Within 1 Business Day"
            }
            };
            return View(product);
        }
    }
}
