using System.Net;

namespace LivratiaPajeu.Exception.ExceptionsBase;

public class ErrorOnValidationException : LivrariaPajeuException
{
    private readonly List<string> _errors;

    public ErrorOnValidationException(List<string> errors) : base(string.Empty)
    {
        _errors = errors;
    }

    public override int StatusCode => (int)HttpStatusCode.BadRequest;

    public override List<string> GetErrors() => _errors;
}
