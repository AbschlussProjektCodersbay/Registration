namespace Registration.service.ServiceChangePreferredAddress.Excepion;

public class UserNotFoundException : System.Exception
{
    public UserNotFoundException()
    {
    }

    public UserNotFoundException(string? message) : base(message)
    {
    }
}
