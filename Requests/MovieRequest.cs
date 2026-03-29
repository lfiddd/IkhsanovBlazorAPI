namespace IkhsanovAPI.Requests;

public class MovieRequest
{
    public int movieId { get; set; }
    public string title { get; set; }
    public string description { get; set; }
    public DateOnly releaseDate { get; set; }
    public float rate { get; set; }
    public int genreId { get; set; }
    public string imageUrl { get; set; }
}