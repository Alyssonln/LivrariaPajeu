using LivrariaPajeu.API.Data;
using LivrariaPajeu.API.Entities;
using LivrariaPajeu.API.Validators;
using LivrariaPajeu.Communication.Requests;
using LivrariaPajeu.Communication.Responses;
using LivratiaPajeu.Exception.ExceptionsBase;

namespace LivrariaPajeu.API.Services;

public class BookService
{
    private readonly BookStorage _storage;

    public BookService(BookStorage storage)
    {
        _storage = storage;
    }

    public ResponseBookJson Create(RequestBookJson request)
    {
        BookValidator.Validate(request);
        EnsureNotDuplicated(request.Title, request.Author);

        var book = new Book
        {
            Title = request.Title.Trim(),
            Author = request.Author.Trim(),
            Genre = request.Genre.Trim().ToLowerInvariant(),
            Price = request.Price,
            Stock = request.Stock
        };

        _storage.Books.Add(book);

        return ToResponse(book);
    }

    public List<ResponseBookJson> GetAll(string? title, string? author, string? genre)
    {
        IEnumerable<Book> books = _storage.Books;

        if (!string.IsNullOrWhiteSpace(title))
            books = books.Where(b => b.Title.Contains(title, StringComparison.OrdinalIgnoreCase));

        if (!string.IsNullOrWhiteSpace(author))
            books = books.Where(b => b.Author.Contains(author, StringComparison.OrdinalIgnoreCase));

        if (!string.IsNullOrWhiteSpace(genre))
            books = books.Where(b => b.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase));

        return books.Select(ToResponse).ToList();
    }

    public ResponseBookJson GetById(Guid id)
    {
        var book = FindBook(id);
        return ToResponse(book);
    }

    public ResponseBookJson Update(Guid id, RequestBookJson request)
    {
        var book = FindBook(id);

        BookValidator.Validate(request);
        EnsureNotDuplicated(request.Title, request.Author, ignoreId: id);

        book.Title = request.Title.Trim();
        book.Author = request.Author.Trim();
        book.Genre = request.Genre.Trim().ToLowerInvariant();
        book.Price = request.Price;
        book.Stock = request.Stock;
        book.UpdatedAt = DateTime.UtcNow;

        return ToResponse(book);
    }

    public void Delete(Guid id)
    {
        var book = FindBook(id);
        _storage.Books.Remove(book);
    }

    private Book FindBook(Guid id)
    {
        return _storage.Books.FirstOrDefault(b => b.Id == id)
            ?? throw new NotFoundException("Livro não encontrado.");
    }

    private void EnsureNotDuplicated(string title, string author, Guid? ignoreId = null)
    {
        var exists = _storage.Books.Any(b =>
            b.Id != ignoreId &&
            b.Title.Equals(title.Trim(), StringComparison.OrdinalIgnoreCase) &&
            b.Author.Equals(author.Trim(), StringComparison.OrdinalIgnoreCase));

        if (exists)
            throw new ConflictException("Já existe um livro com este título e autor.");
    }

    private static ResponseBookJson ToResponse(Book book)
    {
        return new ResponseBookJson
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author,
            Genre = book.Genre,
            Price = book.Price,
            Stock = book.Stock,
            CreatedAt = book.CreatedAt,
            UpdatedAt = book.UpdatedAt
        };
    }
}
