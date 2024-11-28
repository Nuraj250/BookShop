using AdminPanelApp.Data;
using AdminPanelApp.Models;
using Microsoft.AspNetCore.Mvc;
using static System.Reflection.Metadata.BlobBuilder;

namespace AdminPanelApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }
        // View all orders
        public IActionResult Index()
        {
            var orders = _context.Orders.ToList();
            return View("ManageOrder", orders);
        }

        // View a single order and its details
        public IActionResult ViewOrder(Guid id)
        {
            var order = _context.Orders
                .Where(o => o.OrderId == id)
                .Select(o => new
                {
                    o.OrderId,
                    o.CustomerId,
                    o.Date,
                    o.Price,
                    o.PaymentMethod,
                    o.PaymentStatus,
                    o.OrderStatus,
                    OrderDetails = o.OrderDetails.Select(od => new
                    {
                        od.ProductId,
                        od.Quantity,
                        od.Price
                    }).ToList()
                })
                .FirstOrDefault();

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
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
                try
                {
                    var existingOrder = _context.Orders.FirstOrDefault(o => o.OrderId == order.OrderId);
                    if (existingOrder == null)
                    {
                        return NotFound();
                    }

                    // Update order status and payment status
                    existingOrder.PaymentStatus = order.PaymentStatus;
                    existingOrder.OrderStatus = order.OrderStatus;

                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"An error occurred while updating the order: {ex.Message}");
                }
            }
            return View(order);
        }

        // Delete Order
        public IActionResult Delete(Guid id)
        {
            try
            {
                var order = _context.Orders.FirstOrDefault(o => o.OrderId == id);
                if (order == null)
                {
                    return NotFound();
                }

                // Remove associated order details
                var orderDetails = _context.OrderDetails.Where(od => od.OrderId == id).ToList();
                _context.OrderDetails.RemoveRange(orderDetails);

                _context.Orders.Remove(order);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred while deleting the order: {ex.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
