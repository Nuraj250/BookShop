using Microsoft.AspNetCore.Mvc;

namespace AdminPanelApp.Controllers
{
    public class DashBoard : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
