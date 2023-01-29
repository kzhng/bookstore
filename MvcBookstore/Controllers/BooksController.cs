using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcBookstore.Data;
using MvcBookstore.Models;

namespace MvcBookstore.Controllers
{
    public class BooksController : Controller
    {
        private readonly MvcBookstoreContext _context;

        public BooksController(MvcBookstoreContext context)
        {
            _context = context;
        }

        // GET: Books
        /*
         * This is the HTTP get request method for the index method
         * The method checks for null books and will throw an exception.
         * This method is also responsible for the search function for books, as
         * well as search under a specific category using LINQ.
         */
        public async Task<IActionResult> Index(string bookCategory, string searchString)
        {
            if (_context.Book == null)
            {
                return Problem("Entity set 'MvcBookstoreContext.Movie'  is null.");
            }

            // Use LINQ to get list of categories.
            IQueryable<string> categoryQuery = from b in _context.Book
                                               orderby b.Category
                                               select b.Category;
            var books = from b in _context.Book
                        select b;

            if (!string.IsNullOrEmpty(searchString))
            {
                books = books.Where(s => s.Title!.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(bookCategory))
            {
                books = books.Where(x => x.Category == bookCategory);
            }

            var bookCategoryVM = new BookCategoryViewModel
            {
                Categories = new SelectList(await categoryQuery.Distinct().ToListAsync()),
                Books = await books.ToListAsync()
            };

            return View(bookCategoryVM);
        }

        /*
         * HTTP post method for the index action.
         * This method is created to allow users to be able to copy the URL
         * for a given search. It achieves this by posting the filter string
         * to the index action.
         */
        [HttpPost]
        public string Index(string searchString, bool notUsed)
        {
            return "From [HttpPost]Index: filter on " + searchString;
        }

        // GET: Books/Details/5
        /*
         * This method is responsible for the detail action of books.
         * The method checks for null books to prevent hackers from
         * changing the id to an arbitrary one that is not associated
         * with a valid book in the database
         */
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Book == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .FirstOrDefaultAsync(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        /*
         * This method passes an empty book object to the create view, as modifying
         * data in an HTTP GET method is a security risk, as GET requests should not
         * change the state of the app and be a safe operation.
         */
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /*
         * This POST request creates a book object based on the user's input.
         * The bind attribute is used for all fields of the book object to protect against
         * over-posting. 
         */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,BookId,ReleaseDate,Category,Price,Status,ReserveId")] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Edit/5
        /*
         * This method passes the book object with id id to the edit view, as modifying
         * data in an HTTP GET method is a security risk, as GET requests should not
         * change the state of the app and be a safe operation.
         */
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Book == null)
            {
                return NotFound();
            }

            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /*
         * This POST request edits the book object with id id based on the user's input.
         * The bind attribute is used for all fields of the book object to protect against
         * over-posting. 
         */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,BookId,ReleaseDate,Category,Price,Status,ReserveId")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Delete/5
        /*
         * This method passes the book object with id id to the delete view, as modifying
         * data in an HTTP GET method is a security risk, as GET requests should not
         * change the state of the app and be a safe operation. This method does not delete
         * the specified book, but rather returns a view of the book where you can submit an
         * POST request for the deletion.
         */
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Book == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        /*
         * This POST request delete the book object with ID id.
         * It is separated from the GET request as GET requests being able to
         * create, modify or delete data is security risk. 
         *
         */
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Book == null)
            {
                return Problem("Entity set 'MvcBookstoreContext.Book'  is null.");
            }
            var book = await _context.Book.FindAsync(id);
            if (book != null)
            {
                _context.Book.Remove(book);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        // GET: Books/Reserve/5
        /*
         * This method passes the book object with id id to the reserve view, as modifying
         * data in an HTTP GET method is a security risk, as GET requests should not
         * change the state of the app and be a safe operation. This method does not reserve
         * the specified book, but rather returns a view of the book where you can submit an
         * POST request for the reservation.
         */
        public async Task<IActionResult> Reserve(int? id)
        {
            if (id == null || _context.Book == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Reserve/5

        // POST: Books/Delete/5
        /*
         * This POST request reserves the book object with ID id.
         * It is separated from the GET request as GET requests being able to
         * create, modify or delete data is security risk. 
         * This is the overloaded method of the reserve method.
         */
        [HttpPost, ActionName("Reserve")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReserveConfirmed(int id)
        {
            if (_context.Book == null)
            {
                return Problem("Entity set 'MvcBookstoreContext.Book'  is null.");
            }
            var book = await _context.Book.FindAsync(id);
            if (book != null)
            {
                if (book.Status != "Available")
                {
                    ModelState.AddModelError(nameof(Book.Status), "Reservation Error: Unfortunately this book has already been reserved.");
                    return View(book);
                }
                else
                {
                    book.Status = "Reserved";
                    byte[] reserveguid = Guid.NewGuid().ToByteArray();
                    book.ReserveId = BitConverter.ToInt64(reserveguid, 0);
                    _context.Book.Update(book);
                    await _context.SaveChangesAsync();
                }
            }
            return RedirectToAction(nameof(Confirm), new { @id = id });
        }

        // GET: Books/Confirm/5
        /*
         * This method is responsible for the confirm action. It is
         * responsible for displaying the book information and the booking
         * number that is shown to the user upon successfully reserving a book.
         * The method checks the id that is given to the method to ensure it matches
         * up to a valid book in the database.
         */
        public async Task<IActionResult> Confirm(int? id)
        {
            if (id == null || _context.Book == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .FirstOrDefaultAsync(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        private bool BookExists(int id)
        {
            return (_context.Book?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
