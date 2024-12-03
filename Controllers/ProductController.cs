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
            {            };
            return View(product);
        }
    }
}
