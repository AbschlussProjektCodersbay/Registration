namespace Registration.service.ServiceChangePreferredAddress.Excepion;

public class IndexNotInAddressRangeException : System.Exception
{
    public IndexNotInAddressRangeException()
    {
    }

    public IndexNotInAddressRangeException(string? message) : base(message)
    {
    }
}