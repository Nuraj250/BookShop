using Microsoft.AspNetCore.Mvc;
using AdminPanelApp.Models;
using AdminPanelApp.Data;

namespace AdminPanelApp.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BooksController(ApplicationDbContext context)
        {
            _context = context;
        }
        // List all books
        public IActionResult Index()
        {
            var books = _context.Books.ToList();
            return View("ManageBooks", books);
        }

        // Add or Edit Book (GET)
        public IActionResult AddEdit(Guid? id)
        {
            // Create new book if no ID is provided
            if (id == null)
                return View(new Book());

            // Find the book to edit
            var book = _context.Books.FirstOrDefault(b => b.Id == id);
            if (book == null)
                return NotFound();

            return View(book);
        }

        // Add or Edit Book (POST)
        [HttpPost]
        public IActionResult AddEdit(Book book, IFormFile Image)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (book.Id == Guid.Empty) 
                    {
                        if (Image != null)
                            book.ImagePath = SaveImage(Image, "wwwroot/images/books");

                        book.CreatedAt = DateTime.Now;
                        book.UpdatedAt = DateTime.Now;

                        _context.Books.Add(book);
                    }
                    else 
                    {
                        var existingBook = _context.Books.FirstOrDefault(b => b.Id == book.Id);
                        if (existingBook == null)
                            return NotFound();

                        existingBook.Title = book.Title;
                        existingBook.Author = book.Author;
                        existingBook.ISBN = book.ISBN;
                        existingBook.Publisher = book.Publisher;
                        existingBook.Quantity = book.Quantity;
                        existingBook.Price = book.Price;
                        existingBook.Status = book.Status;
                        existingBook.PublishedDate = book.PublishedDate;
                        existingBook.UpdatedAt = DateTime.Now;

                        if (Image != null)
                        {
                            // Delete the old image if a new one is uploaded
                            DeleteImage(existingBook.ImagePath);
                            existingBook.ImagePath = SaveImage(Image, "wwwroot/images/books");
                        }
                    }

                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    // Log the exception and display an error message
                    ModelState.AddModelError("", $"An error occurred while saving the book: {ex.Message}");
                }
            }

            return View(book);
        }

        // Delete Book
        public IActionResult Delete(Guid id)
        {
            try
            {
                var book = _context.Books.FirstOrDefault(b => b.Id == id);
                if (book == null)
                    return NotFound();

                // Delete the associated image if it exists
                DeleteImage(book.ImagePath);

                _context.Books.Remove(book);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Log the exception and redirect to an error page or show a notification
                TempData["ErrorMessage"] = $"An error occurred while deleting the book: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        // Save image to the server
        private string SaveImage(IFormFile image, string folderPath)
        {
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
            var filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                image.CopyTo(stream);
            }

            return $"/images/books/{fileName}";
        }

        // Delete image from the server
        private void DeleteImage(string imagePath)
        {
            if (string.IsNullOrEmpty(imagePath))
                return;

            var filePath = Path.Combine("wwwroot", imagePath.TrimStart('/'));
            if (System.IO.File.Exists(filePath))
                System.IO.File.Delete(filePath);
        }

    }
}
