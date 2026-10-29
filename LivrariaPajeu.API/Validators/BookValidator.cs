using LivrariaPajeu.API.Entities;
using LivrariaPajeu.Communication.Requests;
using LivratiaPajeu.Exception.ExceptionsBase;

namespace LivrariaPajeu.API.Validators;

public class BookValidator
{
    public static void Validate(RequestBookJson request)
    {
        var errors = new List<string>();

        var title = request.Title?.Trim() ?? string.Empty;
        var author = request.Author?.Trim() ?? string.Empty;
        var genre = request.Genre?.Trim() ?? string.Empty;

        if (title.Length < 2 || title.Length > 120)
            errors.Add("O título deve ter entre 2 e 120 caracteres.");

        if (author.Length < 2 || author.Length > 120)
            errors.Add("O autor deve ter entre 2 e 120 caracteres.");

        if (!BookGenres.Valid.Contains(genre, StringComparer.OrdinalIgnoreCase))
            errors.Add($"Gênero inválido. Valores aceitos: {string.Join(", ", BookGenres.Valid)}.");

        if (request.Price < 0)
            errors.Add("O preço deve ser maior ou igual a 0.");

        if (request.Stock < 0)
            errors.Add("O estoque deve ser maior ou igual a 0.");

        if (errors.Count > 0)
            throw new ErrorOnValidationException(errors);
    }
}
