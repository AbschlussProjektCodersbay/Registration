using System.ComponentModel.DataAnnotations;
using MongoDB.Driver;
using Registration.repo;
using Registration.service.Exception;

namespace Registration.service;

public class ValidateData
{
    
    public bool isValid(String email, String password)
    {
        validMail(email);
        checkPassword(password);
        isMailNotExisting(email);
        return true;
    }

    private void validMail(String email)
    {
        var emailValidation = new EmailAddressAttribute();
        if  (!emailValidation.IsValid(email))
        {
            throw new InvalidMailSyntaxException("The Mail Syntax is invalid");
        }
    }

    private void checkPassword(String password)
    {
        if (password.Length < 8)
        {
            throw new InvalidPasswordException("The password is to low");
        }
        
    }

    
    private void isMailNotExisting(String mail)
    {
        var repo = new RepoNewUser();
        var collection = repo.GetUserCollection();
        var foundMail =  collection.FindSync((user) => user.email == mail).ToList();
        Console.WriteLine(foundMail.Count);
        
        if (foundMail.Count > 0)
        {
            throw new MailExistException("The Mail " + mail + " exist");
        }
        
    }
    
    
     
}