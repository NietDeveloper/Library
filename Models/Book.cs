public class Book
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Genre { get; set; }
    public string? Author { get; set; }
    public int TotalCopies { get; set; }
    public int AvailableCopies { get; set; }
    public List<string?> Readers { get; set; } = new List<string?>();
}