using Microsoft.AspNetCore.Mvc.Rendering;

namespace MvcBookstore.Models
{
    public class BookCategoryViewModel
    {
        public List<Book>? Books { get; set; }
        public SelectList? Categories { get; set; }
        public string? BookCategory { get; set; }
        public string? SearchString { get; set; }
    }
}
