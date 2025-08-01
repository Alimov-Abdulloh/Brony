namespace Brony.Exceptions;

public class NotFoundException : Exception
{
    public int StatusCode { get; }
    public NotFoundException(string message) : base(message)
    {
        StatusCode = 404;
    }
}

public class ArgumentIsNotValidException : Exception
{
    public int StatusCode { get; }

    public ArgumentIsNotValidException(string message) : base(message)
    {
        StatusCode = 400;
    }
}