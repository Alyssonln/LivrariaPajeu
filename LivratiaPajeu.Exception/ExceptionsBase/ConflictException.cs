using System.Net;

namespace LivratiaPajeu.Exception.ExceptionsBase;

public class ConflictException : LivrariaPajeuException
{
    public ConflictException(string message) : base(message) { }

    public override int StatusCode => (int)HttpStatusCode.Conflict;
    public override List<string> GetErrors() => [Message];
}
