namespace MinimalApi.Models;

public class Item
{
    public int Id { get; set; }
    public required string Name { get; set; } = string.Empty;
}