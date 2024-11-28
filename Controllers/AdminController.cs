using AdminPanelApp.Data;
using AdminPanelApp.Models;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace AdminPanelApp.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ManageBooks()
        {
            return RedirectToAction("Index", "Books");
        }

        public IActionResult ManageUsers()
        {
            return RedirectToAction("Index", "Users");
        }
        public IActionResult ManageOrders()
        {
            return RedirectToAction("Index", "Order");
        }
    }
}
