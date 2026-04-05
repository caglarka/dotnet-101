namespace LevelOne.API.Models;

public record BookRecord
{
    // Otomatik özellikler (init-only properties)
    public string Title { get; set; }
    public string Author { get; set; }
    public int Price { get; set; }

    public BookRecord(string title, string author, int price)
    {
        Title = title;
        Author = author;
        Price = price;
    }

    public string DisplayInfo()
    {
        return $"Title: {Title}, Author: {Author}, Price: {Price}";
    }
}
