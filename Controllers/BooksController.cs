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
        public IActionResult Index()
        {
            var books = _context.Books.ToList();
            return View(books);
        }
        // Add/Edit Book (GET)
        public IActionResult AddEdit(Guid? id)
        {
            if (id == null) // Add new book
            {
                return View(new Book());
            }
            else // Edit existing book
            {
                var book = _context.Books.FirstOrDefault(b => b.Id == id);
                if (book == null)
                {
                    return NotFound();
                }
                return View(book);
            }
        }

        // Add/Edit Book (POST)
        [HttpPost]
        public IActionResult AddEdit(Book book, IFormFile Image)
        {
            if (ModelState.IsValid)
            {
                if (book.Id == Guid.Empty) // Add new book
                {
                    if (Image != null && Image.Length > 0)
                    {
                        var fileName = Path.GetFileName(Image.FileName);
                        var filePath = Path.Combine("wwwroot/images/books", fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            Image.CopyTo(stream);
                        }

                        book.ImagePath = $"/images/books/{fileName}";
                    }

                    book.CreatedAt = DateTime.Now;
                    book.UpdatedAt = DateTime.Now;

                    _context.Books.Add(book);
                }
                else // Update existing book
                {
                    var existingBook = _context.Books.FirstOrDefault(b => b.Id == book.Id);
                    if (existingBook != null)
                    {
                        existingBook.Title = book.Title;
                        existingBook.Author = book.Author;
                        existingBook.ISBN = book.ISBN;
                        existingBook.Publisher = book.Publisher;
                        existingBook.Quantity = book.Quantity;
                        existingBook.Price = book.Price;
                        existingBook.Status = book.Status;
                        existingBook.PublishedDate = book.PublishedDate;
                        existingBook.UpdatedAt = DateTime.Now;

                        if (Image != null && Image.Length > 0)
                        {
                            var fileName = Path.GetFileName(Image.FileName);
                            var filePath = Path.Combine("wwwroot/images/books", fileName);

                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                Image.CopyTo(stream);
                            }

                            existingBook.ImagePath = $"/images/books/{fileName}";
                        }
                    }
                }

                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(book);
        }

        // Delete Book
        public IActionResult Delete(Guid id)
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == id);
            if (book != null)
            {
                if (!string.IsNullOrEmpty(book.ImagePath))
                {
                    var imagePath = Path.Combine("wwwroot", book.ImagePath.TrimStart('/'));
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }

                _context.Books.Remove(book);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    
    }
}
