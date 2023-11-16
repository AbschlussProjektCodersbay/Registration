namespace Registration.service.ServiceLogin.Exception;

public class LoginNotValid : System.Exception
{
    public LoginNotValid()
    {
    }

    public LoginNotValid(string? message) : base(message)
    {
    }
}
