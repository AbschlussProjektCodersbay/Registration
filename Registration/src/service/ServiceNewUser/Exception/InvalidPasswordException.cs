namespace Registration.service.Exception;

public class InvalidPasswordException : System.Exception
{
    public InvalidPasswordException()
    {
    }

    public InvalidPasswordException(string? message) : base(message)
    {
    }
}