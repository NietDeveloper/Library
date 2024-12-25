public class User
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public int Age { get; set; }
    public string? Login { get; set; }
    public string? Password { get; set; }
    public int BorrowedBookCount { get; set; }
    public List<string?> BorrowedBooks { get; set; } = new List<string?>();
}