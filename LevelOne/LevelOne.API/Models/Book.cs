namespace LevelOne.API.Models;

// Temel sınıf base Class
public class Book
{
    // Kapsülleme (Encapsulation): private erişim belileyici ile özelliklere erişim kısıtlandı.
    private string _title;
    private string _author;
    private int _price;

    // Propertyler aracılığıyla kapsülleme yapılmış verilere erişim
    public string Title
    {
        get { return _title; }
        set
        {
            _title = value;
        }
    }

    public string Author
    {
        get { return _author; }
        set
        {
            _author = value;
        }
    }

    public int Price
    {
        get { return _price; }
        set
        {
            _price = value;
        }
    }

    // Constructor
    public Book(string title, string author, int price)
    {
        Title = title;
        Author = author;
        Price = price;
    }

    public string DispolayInfo()
    {
        return $"Title: {Title}, Author: {Author}, Price: {Price}";
    }

    // Polymorphism: Virtual metot tanımlama
    public virtual string PrintType()
    {
        return "Bu basılı bir kitaptır.";
    }
}

