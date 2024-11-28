using Microsoft.AspNetCore.Mvc;
using AdminPanelApp.Models;
using AdminPanelApp.Data;

namespace AdminPanelApp.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // List all users
        public IActionResult Index()
        {
            var users = _context.Users.ToList();
            return View(users);
        }

        // Add/Edit User (GET)
        public IActionResult AddEdit(Guid? id)
        {
            if (id == null) // Add new user
            {
                return View(new User());
            }
            else // Edit existing user
            {
                var user = _context.Users.FirstOrDefault(u => u.Id == id);
                if (user == null)
                {
                    return NotFound();
                }
                return View(user);
            }
        }

        // Add/Edit User (POST)
        [HttpPost]
        public IActionResult AddEdit(User user, IFormFile Image)
        {
            if (ModelState.IsValid)
            {
                if (user.Id == Guid.Empty) // Add new user
                {
                    if (Image != null && Image.Length > 0)
                    {
                        var fileName = Path.GetFileName(Image.FileName);
                        var filePath = Path.Combine("wwwroot/images/users", fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            Image.CopyTo(stream);
                        }

                        user.ImagePath = $"/images/users/{fileName}";
                    }

                    user.CreatedAt = DateTime.Now;
                    user.UpdatedAt = DateTime.Now;

                    _context.Users.Add(user);
                }
                else // Update existing user
                {
                    var existingUser = _context.Users.FirstOrDefault(u => u.Id == user.Id);
                    if (existingUser != null)
                    {
                        existingUser.Name = user.Name;
                        existingUser.Email = user.Email;
                        existingUser.PhoneNumber = user.PhoneNumber;
                        existingUser.Role = user.Role;
                        existingUser.UpdatedAt = DateTime.Now;

                        if (Image != null && Image.Length > 0)
                        {
                            var fileName = Path.GetFileName(Image.FileName);
                            var filePath = Path.Combine("wwwroot/images/users", fileName);

                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                Image.CopyTo(stream);
                            }

                            existingUser.ImagePath = $"/images/users/{fileName}";
                        }
                    }
                }

                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // Delete User
        public IActionResult Delete(Guid id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                if (!string.IsNullOrEmpty(user.ImagePath))
                {
                    var imagePath = Path.Combine("wwwroot", user.ImagePath.TrimStart('/'));
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }

                _context.Users.Remove(user);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
