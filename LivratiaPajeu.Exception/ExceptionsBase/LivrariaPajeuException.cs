namespace LivratiaPajeu.Exception.ExceptionsBase;

public abstract class LivrariaPajeuException : SystemException
{
    protected LivrariaPajeuException(string message) : base(message) { }

    public abstract int StatusCode { get; }
    public abstract List<string> GetErrors();
}
