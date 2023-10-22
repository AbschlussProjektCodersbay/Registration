namespace Registration.service.Exception;

[Serializable]
public class MailExistException : System.Exception
{
    
    public MailExistException() { }

    public MailExistException(string message)
            : base(message) { }
    
}