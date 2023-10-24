namespace Registration.service.ServiceGetCheckoutData.Exceptions;

public class UserHaveNoAddressException : System.Exception
{
    public UserHaveNoAddressException()
    {
    }

    public UserHaveNoAddressException(string? message) : base(message)
    {
    }
}