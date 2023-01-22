using System.ComponentModel.DataAnnotations;

namespace MvcBookstore.Models;

public class Book
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? BookId { get; set; }
    [DataType(DataType.Date)]
    public DateTime ReleaseDate { get; set; }
    public string? Category { get; set; }
    public decimal Price { get; set; }
}