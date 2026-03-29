using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IkhsanovAPI.Models;

public class Movie
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int movieId { get; set; }
    public string title { get; set; }
    public string description { get; set; }
    public DateOnly releaseDate { get; set; }
    
    [Required]
    [ForeignKey("Genre")]
    public int genreId { get; set; }
    public  Genre Genre { get; set; }
    
    public float rate { get; set; }
    public string imageUrl { get; set; }
}