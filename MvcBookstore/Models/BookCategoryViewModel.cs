using Microsoft.AspNetCore.Mvc.Rendering;

namespace MvcBookstore.Models
{
    /*
     * This class is responsible for the view model of the search function in the index page of books.
     *
     */
    public class BookCategoryViewModel
    {
        public List<Book>? Books { get; set; }
        public SelectList? Categories { get; set; }
        public string? BookCategory { get; set; }
        public string? SearchString { get; set; }
    }
}
