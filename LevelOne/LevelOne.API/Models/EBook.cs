namespace LevelOne.API.Models;

public class EBook : Book
{
    public string Format { get; set; }

    // Constructor
    public EBook(string title, string author, int price, string format)
        : base(title, author, price)
    {
        Format = format;
    }

    // Polymorphism: Temel sınıftan devralınan override etme
    public override string PrintType()
    {
        return "Bu bir elektronik bir kitaptır.";
    }
}
