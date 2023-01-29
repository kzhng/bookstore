using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcBookstore.Models;

/*
 * The class responsible for the book object.
 * It has various attributes such as title, book id, and reservation id.
 * Validation rules have been set for ost attributes to prevent users from
 * supplying junk data when they are creating or editing books.
 */
public class Book
{
    //Unique identifier for the book (Primary Key)
    public int Id { get; set; }

    //Title of the book
    [StringLength(70, MinimumLength = 3)]
    [Required]
    public string? Title { get; set; }

    //the book id 
    [StringLength(100, MinimumLength = 3)]
    [Required]
    [Display(Name = "Book ID")]
    public string? BookId { get; set; }

    //The release date of the book
    [Display(Name = "Release Date")]
    [DataType(DataType.Date)]
    public DateTime ReleaseDate { get; set; }

    //The category of the book
    [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
    [Required]
    [StringLength(30)]
    public string? Category { get; set; }

    //The price of the book
    [Range(1, 500)]
    [DataType(DataType.Currency)]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    /*
     * The reservation status of the book. If unspecified, it will default to available.
     * Books can either be available or reserved.
     */
    [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
    [Required]
    [StringLength(10)]
    public string? Status { get; set; } = "Available";

    /*
     * The booking number or reservation id for a given book. Default value is null, however
     * if a book has been reserved it will have a unique reservation id. (Foreign key)
     */
    [Display(Name = "Reservation ID")]
    public long? ReserveId { get; set; }
}