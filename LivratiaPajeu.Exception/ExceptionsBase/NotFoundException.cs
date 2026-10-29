using System.Net;

namespace LivratiaPajeu.Exception.ExceptionsBase;

public class NotFoundException : LivrariaPajeuException
{
    public NotFoundException(string message) : base(message) { }

    public override int StatusCode => (int)HttpStatusCode.NotFound;
    public override List<string> GetErrors() => [Message];
}
