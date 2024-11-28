using AdminPanelApp.Data;
using AdminPanelApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanelApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var orders = _context.Orders.ToList();
            return View(orders);
        }
        // Edit Order (GET)
        public IActionResult Edit(Guid id)
        {
            var order = _context.Orders.FirstOrDefault(o => o.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // Edit Order (POST)
        [HttpPost]
        public IActionResult Edit(Order order)
        {
            if (ModelState.IsValid)
            {
                var existingOrder = _context.Orders.FirstOrDefault(o => o.OrderId == order.OrderId);
                if (existingOrder != null)
                {
                    existingOrder.PaymentStatus = order.PaymentStatus;
                    existingOrder.OrderStatus = order.OrderStatus;

                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(order);
        }

        // Delete Order
        public IActionResult Delete(Guid id)
        {
            var order = _context.Orders.FirstOrDefault(o => o.OrderId == id);
            if (order != null)
            {
                // Remove associated order details
                var orderDetails = _context.OrderDetails.Where(od => od.OrderId == id).ToList();
                _context.OrderDetails.RemoveRange(orderDetails);

                _context.Orders.Remove(order);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
