namespace IkhsanovAPI.Requests;

public class QueryUsers
{
    public int userId { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string fullname { get; set; }
    public string description { get; set; }
}