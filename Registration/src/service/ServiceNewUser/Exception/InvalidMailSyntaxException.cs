namespace Registration.service.Exception;

public class InvalidMailSyntaxException : System.Exception
{
    public InvalidMailSyntaxException()
    {
    }

    public InvalidMailSyntaxException(string? message) : base(message)
    {
    }
}