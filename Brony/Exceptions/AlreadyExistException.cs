namespace Brony.Exceptions;

public class AlreadyExistException : Exception
{
    public int StatusCode { get; }

    public AlreadyExistException(string message) : base(message)
    {
        StatusCode = 409;
    }
}