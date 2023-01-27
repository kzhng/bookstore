using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcBookstore.Models;

public class Book
{
    public int Id { get; set; }
    public string? Title { get; set; }
    [Display(Name = "Book ID")]
    public string? BookId { get; set; }
    [Display(Name = "Release Date")]
    [DataType(DataType.Date)]
    public DateTime ReleaseDate { get; set; }
    public string? Category { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    public string? Status { get; set; } = "Available";
}