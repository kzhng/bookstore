using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcBookstore.Models;

public class Book
{
    public int Id { get; set; }

    [StringLength(70, MinimumLength = 3)]
    [Required]
    public string? Title { get; set; }

    [StringLength(100, MinimumLength = 3)]
    [Required]
    [Display(Name = "Book ID")]
    public string? BookId { get; set; }

    [Display(Name = "Release Date")]
    [DataType(DataType.Date)]
    public DateTime ReleaseDate { get; set; }

    [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
    [Required]
    [StringLength(30)]
    public string? Category { get; set; }

    [Range(1, 500)]
    [DataType(DataType.Currency)]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
    [Required]
    [StringLength(10)]
    public string? Status { get; set; } = "Available";
}