using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

using System.Diagnostics;
using MongoDB.Driver;
using Registration.model;
using Registration.repo;
using Registration.service.ServiceLogin.Exception;

namespace Registration.service.ServiceLogin;

public class ServiceLogin
{
    private string? UserId;
    
    public bool CheckUser(Stream data)
    {
        try
        {
            var userLoginData = dataToUser(data);
            var passwordHash = findUser(userLoginData.email);
            var isValid = HashManager.VerifyHash(userLoginData.password, passwordHash);
            return isValid;
        }
        catch(System.Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
        
    }

    
    
    private string findUser(string email)
    {
        email = email.ToLower();
        var repo = new RepoNewUser();
        var collection = repo.GetUserCollection();
        var condition = Builders<ModelNewUser>.Filter.Eq((user) => user.email, email);
        var field = Builders<ModelNewUser>.Projection
            .Include(user => user.password)
            .Include(user => user.Id);
        
        var results = collection.Find(condition).Project<ModelNewUser>(field).ToList();
        if (results.Count == 0)
        {
            throw new LoginNotValid();
        }
        
        
        UserId = results[0].Id;
        if (UserId == null)
        {
            Console.WriteLine("user have no Id");
            throw new LoginNotValid();
        }
        
        return results[0].password;
    }

    private ModelNewUser dataToUser(Stream userData)
    {
        using var reader = new StreamReader(userData);
        var body = reader.ReadToEndAsync().Result;
        var userModel = Newtonsoft.Json.JsonConvert.DeserializeObject<ModelNewUser>(body);
        
        if (userModel==null)
        {
            throw new LoginNotValid();
        }
        return userModel;
        
    }

    public string GetUserId()
    {
        return UserId;
    }
}