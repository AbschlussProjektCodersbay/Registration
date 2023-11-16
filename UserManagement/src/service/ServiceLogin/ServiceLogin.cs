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

    public bool CheckUser(Stream data)
    {
        try
        {
            
            

        }
        catch(System.Exception e)
        {
            Console.WriteLine(e.Message);
            return false;
        }
        var userLoginData = dataToUser(data);
        var passwordHash = findUser(userLoginData.email);
        var test = HashManager.VerifyHash(userLoginData.password, passwordHash);
        return test;
    }

    
    
    private string findUser(string email)
    {
        email = email.ToLower();
        var repo = new RepoNewUser();
        var collection = repo.GetUserCollection();
        var condition = Builders<ModelNewUser>.Filter.Eq((user) => user.email, email);
        var field = Builders<ModelNewUser>.Projection
            .Include(p => p.password);
        
        var results = collection.Find(condition).Project<ModelNewUser>(field).ToList();
        if (results.Count == 0)
        {
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
    
    

}