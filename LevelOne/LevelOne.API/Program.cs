using LevelOne.API.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

// Swagger için gerekli implementasyonlar
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


app.MapGet("/temp-class", () =>
{
    // Temel sınıf örneği
    var book = new Book("Title", "Author", 10);
    var ds = book.DispolayInfo();
    var pr = book.PrintType();

    var res = ds + " " + pr;

    // Türetilmiş sınıf örneği
    var eBook = new EBook("E-Title", "E-Author", 7, "E-Format");
    var eDs = eBook.DispolayInfo();
    var ePr = eBook.PrintType();

    var eRes = eDs + " " + ePr;

    // Record Örneği
    var rBook = new BookRecord("Title", "Author", 10);
    var rDs = rBook.DisplayInfo();


    return Results.Ok(rDs);
})
.WithName("Class Example");

app.UseSwagger();
app.UseSwaggerUI();

app.Run();


